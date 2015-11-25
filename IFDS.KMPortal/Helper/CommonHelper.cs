#region Namespaces
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace IFDS.KMPortal.Helper
{
    public static class  CommonHelper
    {
        #region Global Declaration Section

        /// <summary>
        /// Global variable declaration Section.
        /// </summary>
        static ClientContext clientContext;
        static string listName;
        static string displayCount;

        #endregion

        #region Common Section

        /// <summary>
        /// This method loads the clientcontext and fetches list and item count properties from web part.
        /// </summary>
        /// <returns></returns>
        public static bool GetClientContext(string propListName, string propItemCount, string propSiteUrl)
        {
            bool isExists = false;
            try
            {
                clientContext = new ClientContext(propSiteUrl);
                clientContext.Credentials = new NetworkCredential("dstcorfax\\cg091089", "Maddy1984");
               // clientContext.Credentials = CredentialCache.DefaultCredentials;
                bool isvalid = GetListByTitle(propListName);
                if (isvalid)
                {
                    listName = propListName;
                    displayCount = propItemCount;
                    isExists = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetClientContext"), ex.StackTrace);
            }
            return isExists;
        }

        /// <summary>
        /// This method validate if thee list exists in sharepoint site.
        /// returns true if exists elsereturns false.
        /// </summary>
        /// <param name="clientcontext"></param>
        /// <param name="listTitle"></param>
        /// <returns></returns>
        public static bool GetListByTitle(String listTitle)
        {
            bool isValidList = false;
            try
            {
                List existingList;
                Web web = clientContext.Web;
                ListCollection lists = web.Lists;
                IEnumerable<List> existingLists = clientContext.LoadQuery(
                        lists.Where(
                        list => list.Title == listTitle)
                        );
                clientContext.ExecuteQuery();
                existingList = existingLists.FirstOrDefault();
                if (existingList != null)
                {
                    isValidList = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetListByTitle"), ex.StackTrace);
            }
            return isValidList;
        }

        /// <summary>
        /// This method returns list sever relative url.
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public static string[] GetListUrlByTitle(string listName)
        {
            string[] listURL = new string[2];
            try
            {
                Web oWeb = clientContext.Web;
                List oList = clientContext.Web.Lists.GetByTitle(listName);
                clientContext.Load(oList.RootFolder);
                clientContext.ExecuteQuery();
                listURL[0] = oList.RootFolder.ServerRelativeUrl;
                listURL[1] = oList.RootFolder.ServerRelativeUrl + "/NewForm.aspx";
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetListUrlByTitle"), ex.StackTrace);
            }

            return listURL;
        }

        #endregion

        #region Global Discussions Forum Section

        /// <summary>
        /// This method executes te search api to get most popular items from the current site
        /// </summary>
        /// <param name="clientContext"></param>
        /// <param name="propListName"></param>
        /// <param name="errMSG"></param>
        /// <returns></returns>
        public static string GetDiscussionsFromSearchAPI(string propListName, string errMSG)
        {
            string mostPopular = string.Empty;
            try
            {

                if (CacheHelper.IsIncache("Global_MostPopularDiscussion"))
                {
                    mostPopular = (string)CacheHelper.GetFromCache("Global_MostPopularDiscussion");
                
                }
                else
                {
                Web oWeb = clientContext.Web;
                clientContext.Load(oWeb);
                clientContext.ExecuteQuery();
                KeywordQuery keywordQuery = new KeywordQuery(clientContext);
                Uri uri = new Uri(oWeb.Url.ToString());
                string urlPath = System.Web.HttpUtility.UrlPathEncode(uri.GetLeftPart(UriPartial.Authority) + GetListUrlByTitle(propListName)[0]);
                string actualURL = string.Format(@"path:{0}", urlPath);
                StringCollection selectProperties = keywordQuery.SelectProperties;
                selectProperties.Add("Created");
                selectProperties.Add("FileExtension");
                selectProperties.Add("ContentTypeId");
                selectProperties.Add("Title");
                selectProperties.Add("Author");
                selectProperties.Add("Path");
                selectProperties.Add("ReplyCount");
                keywordQuery.QueryText = actualURL;
                SearchExecutor searchExecutor = new SearchExecutor(clientContext);
                ClientResult<ResultTableCollection> results = searchExecutor.ExecuteQuery(keywordQuery);
                clientContext.ExecuteQuery();
                mostPopular = GenerateDiscussionHTML(results, errMSG);
                }
            }
            catch (Exception ex)
            {
                mostPopular = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetDiscussionsFromSearchAPI"), ex.StackTrace);
            }

            return mostPopular;
        }

        /// <summary>
        /// This method generates dynamic html based on the list item collection.
        /// </summary>
        /// <param name="clientcontext"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GenerateDiscussionHTML(ClientResult<ResultTableCollection> items, string errMsg)
        {
            string mostPopularHTML = string.Empty;
            StringBuilder oSb = new StringBuilder();
            try
            {
                int count = 0;

                foreach (var resultRow in items.Value[0].ResultRows)
                {
                    if (count < Convert.ToInt32(displayCount))
                    {
                        bool allowOnly = (!string.IsNullOrEmpty(Convert.ToString(resultRow["Created"])) && !Convert.ToString(resultRow["FileExtension"]).Contains("aspx") && !string.IsNullOrEmpty(Convert.ToString(resultRow["ContentTypeId"])));

                        if (allowOnly)
                        {
                            DateTime dt = (DateTime)resultRow["Created"];
                            string formattedDate = string.Format("{0:dd MMM yyyy}", dt);
                            oSb.Append("<div class='docinfo'>");
                            oSb.Append("<div class='forum-txt'>");
                            oSb.Append("<a href='" + resultRow["Path"] + "'>");
                            oSb.Append("<p class='skyblue'>" + resultRow["Title"] + "</p>");
                            oSb.Append("</a>");
                            oSb.Append("</div>");
                            oSb.Append("<div class='userinfo-forum'>");
                            oSb.Append("<div class='usrinfo-box'>");
                            oSb.Append("<a href='#' class=''>");
                            oSb.Append("<span class='blue user-txt-detail'>" + resultRow["Author"].ToString() + "</span>");
                            oSb.Append("</a>");
                            oSb.Append("<div class='icon-time-user'>");
                            oSb.Append("<span class='glyphicon glyphicon-time' aria-hidden='true'><span class='icon-txt'>" + formattedDate + "</span></span>");
                            oSb.Append("<span class='glyphicon glyphicon-comment' aria-hidden='true'><span class='icon-txt'> " + resultRow["ReplyCount"] + " Comments</span></span>");
                            oSb.Append("</div>");
                            oSb.Append("</div>");
                            oSb.Append("</div>");
                            oSb.Append("</div>");
                        }
                        else
                        {
                            count--;
                        }
                    }
                    count++;
                }

                if (oSb.Length > 0)
                {

                    // Check If Global_MostPopularDiscussion cache exists
                    
                        CacheHelper.SaveTocache("Global_MostPopularDiscussion",oSb.ToString(),DateTime.Now.AddSeconds(60.00));

                   
                    mostPopularHTML = oSb.ToString();
                }
            }
            catch (Exception ex)
            {
                mostPopularHTML = errMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GenerateDiscussionHTML"), ex.StackTrace);
            }
            return mostPopularHTML;
        }

        /// <summary>
        /// This method executes te search api to get most popular items from the current site
        /// </summary>
        /// <param name="clientContext"></param>
        /// <param name="propListName"></param>
        /// <param name="errMSG"></param>
        /// <returns></returns>
        public static string GetRecentDiscussionsFromCAMLQuery(string propListName, string propDisplayCount, string errMSG)
        {
            string mostRecent = string.Empty;
            try
            {

                if (CacheHelper.IsIncache("Global_RecentDiscussion"))
                {
                    mostRecent = (string)CacheHelper.GetFromCache("Global_RecentDiscussion");

                }
                else
                {
                    List oList = clientContext.Web.Lists.GetByTitle(propListName);
                    CamlQuery camlQuery = new CamlQuery();
                    camlQuery.ViewXml = "<View><Query><OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy></Query><RowLimit>" + propDisplayCount + "</RowLimit></View>";
                    ListItemCollection items = oList.GetItems(camlQuery);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();
                    mostRecent = GenerateRecentDiscussionHTML(items, errMSG);
                }
            }
            catch (Exception ex)
            {
                mostRecent = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetRecentDiscussionsFromCAMLQuery"), ex.StackTrace);
            }

            return mostRecent;
        }

        /// <summary>
        /// This method generates dynamic html based on the list item collection.
        /// </summary>
        /// <param name="clientcontext"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GenerateRecentDiscussionHTML(ListItemCollection oItems, string errMSG)
        {
            string mostRecentHTML = string.Empty;

            StringBuilder oSb = new StringBuilder();
            try
            {
                foreach (ListItem oItem in oItems)
                {
                    DateTime dt = (DateTime)oItem["Created"];
                    string formattedDate = string.Format("{0:dd MMM yyyy}", dt);
                    oSb.Append("<div class='docinfo'>");
                    oSb.Append("<div class='forum-txt'>");
                    oSb.Append("<a href='" + clientContext.Url + oItem["FileRef"] + "'>");
                    oSb.Append("<p class='skyblue'>" + oItem["Title"] + "</p>");
                    oSb.Append("</a>");
                    oSb.Append("</div>");
                    oSb.Append("<div class='userinfo-forum'>");
                    oSb.Append("<div class='usrinfo-box'>");
                    oSb.Append("<a href='#' class='pull-left'>");
                    oSb.Append("<span class='blue user-txt-detail'>" + ((FieldUserValue)oItem["Author"]).LookupValue.ToString() + "</span>");
                    oSb.Append("</a>");
                    oSb.Append("<div class='icon-time-user'>");
                    oSb.Append("<span class='glyphicon glyphicon-time' aria-hidden='true'><span class='icon-txt'>" + formattedDate + "</span></span>");
                    oSb.Append("<span class='glyphicon glyphicon-comment' aria-hidden='true'><span class='icon-txt'> " + oItem["ItemChildCount"] + " Comments</span></span>");
                    oSb.Append("</div>");
                    oSb.Append("</div>");
                    oSb.Append("</div>");
                    oSb.Append("</div>");
                }

                if (oSb.Length > 0)
                {

                    // Check If Global_MostPopularDiscussion cache exists

                    CacheHelper.SaveTocache("Global_RecentDiscussion", oSb.ToString(), DateTime.Now.AddSeconds(60.00));


                    mostRecentHTML = oSb.ToString();
                }
               
            }
            catch (Exception ex)
            {
                mostRecentHTML = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GenerateRecentDiscussionHTML"), ex.StackTrace);
            }
            return mostRecentHTML;
        }

        #endregion

        #region Global FAQ's Section

        /// <summary>
        /// This method executes te search api to get most popular items from the current site
        /// </summary>
        /// <param name="clientContext"></param>
        /// <param name="propListName"></param>
        /// <param name="errMSG"></param>
        /// <returns></returns>
        public static string GetFAQFromSearchAPI(string propListName, string errMSG)
        {
            string mostPopular = string.Empty;
            try
            {
                Web oWeb = clientContext.Web;
                clientContext.Load(oWeb);
                clientContext.ExecuteQuery();
                KeywordQuery keywordQuery = new KeywordQuery(clientContext);
                Uri uri = new Uri(oWeb.Url.ToString());
                string urlPath = System.Web.HttpUtility.UrlPathEncode(uri.GetLeftPart(UriPartial.Authority) + GetListUrlByTitle(propListName)[0]);
                string actualURL = string.Format(@"path:{0}", urlPath);
                StringCollection selectProperties = keywordQuery.SelectProperties;
                selectProperties.Add("LastModifiedTime");
                selectProperties.Add("ContentTypeId");
                selectProperties.Add("Title");
                selectProperties.Add("Answer");
                keywordQuery.QueryText = actualURL;
                SearchExecutor searchExecutor = new SearchExecutor(clientContext);
                ClientResult<ResultTableCollection> results = searchExecutor.ExecuteQuery(keywordQuery);
                clientContext.ExecuteQuery();
                mostPopular = GenerateFAQHTML(results, errMSG);
            }
            catch (Exception ex)
            {
                mostPopular = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetFAQFromSearchAPI"), ex.StackTrace);
            }

            return mostPopular;
        }

        /// <summary>
        /// This method generates dynamic html based on the list item collection.
        /// </summary>
        /// <param name="clientcontext"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GenerateFAQHTML(ClientResult<ResultTableCollection> items, string errMsg)
        {
            string mostPopularHTML = string.Empty;
            StringBuilder oSb = new StringBuilder();
            try
            {
                int count = 0;
                int i = 1;
                foreach (var resultRow in items.Value[0].ResultRows)
                {
                    if (count < Convert.ToInt32(displayCount))
                    {
                        bool allowOnly = (!string.IsNullOrEmpty(Convert.ToString(resultRow["LastModifiedTime"])) && !string.IsNullOrEmpty(Convert.ToString(resultRow["ContentTypeId"])));

                        if (allowOnly)
                        {
                            DateTime dt = (DateTime)resultRow["LastModifiedTime"];
                            string formattedDate = string.Format("{0:dd MMM yyyy}", dt);
                            oSb.Append("<div class='panel panel-default faq-acordian'>");
                            oSb.Append("<div class='panel-heading'>");
                            oSb.Append("<a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapsePop" + i + "'>");
                            oSb.Append("<h4 class='panel-title'>");
                            oSb.Append("<i class='acc-con'>");
                            oSb.Append("<span class='glyphicon glyphicon-chevron-down'></span></i>");
                            oSb.Append("<p class='accHeadtxt'><span class='skyblue'>" + formattedDate + " </span>" + resultRow["Title"] + "</p>");
                            oSb.Append("</h4>");
                            oSb.Append("</a>");
                            oSb.Append("</div>");
                            oSb.Append("<div id='collapsePop" + i + "' class='panel-collapse collapse in'>");
                            oSb.Append("<div class='panel-body acc-content'>");
                            oSb.Append("<p>" + resultRow["Answer"] + "</p>");
                            oSb.Append("</div>");
                            oSb.Append("</div>");
                            oSb.Append("</div>");
                            i++;
                        }
                        else
                        {
                            count--;
                        }
                    }
                    count++;
                }
                mostPopularHTML = oSb.ToString();
            }
            catch (Exception ex)
            {
                mostPopularHTML = errMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GenerateFAQHTML"), ex.StackTrace);
            }
            return mostPopularHTML;
        }

        /// <summary>
        /// This method executes te search api to get most popular items from the current site
        /// </summary>
        /// <param name="clientContext"></param>
        /// <param name="propListName"></param>
        /// <param name="errMSG"></param>
        /// <returns></returns>
        public static string GetRecentFAQFromCAMLQuery(string propListName, string propDisplayCount, string errMSG)
        {
            string mostRecent = string.Empty;
            try
            {
                List oList = clientContext.Web.Lists.GetByTitle(propListName);
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml = "<View><Query><OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy></Query><RowLimit>" + propDisplayCount + "</RowLimit></View>";
                ListItemCollection items = oList.GetItems(camlQuery);
                clientContext.Load(items);
                clientContext.ExecuteQuery();
                mostRecent = GenerateRecentFAQHTML(items, errMSG);
            }
            catch (Exception ex)
            {
                mostRecent = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetRecentFAQFromCAMLQuery"), ex.StackTrace);
            }

            return mostRecent;
        }

        /// <summary>
        /// Get FAQ's from all sites and populate in Global FAQ widget
        /// if Global site property is checked, then this function will execute
        /// </summary>
        /// <returns></returns>
        public static string GetGlobalPopularFaq(string propListName, string propDisplayCount, string errMSG)
        {
            string mostRecent = string.Empty;
            try
            {
                List oList = clientContext.Web.Lists.GetByTitle(propListName);
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml = "<View><Query><OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy></Query><RowLimit>" + propDisplayCount + "</RowLimit></View>";
                ListItemCollection items = oList.GetItems(camlQuery);
                clientContext.Load(items);
                clientContext.ExecuteQuery();
                mostRecent = GenerateRecentFAQHTML(items, errMSG);
            }
            catch (Exception ex)
            {
                mostRecent = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetRecentFAQFromCAMLQuery"), ex.StackTrace);
            }

            return mostRecent;
        }

        /// <summary>
        /// This method generates dynamic html based on the list item collection.
        /// </summary>
        /// <param name="clientcontext"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GenerateRecentFAQHTML(ListItemCollection oItems, string errMSG)
        {
            string mostRecentHTML = string.Empty;

            StringBuilder oSb = new StringBuilder();
            try
            {
                int i = 1;
                foreach (ListItem oItem in oItems)
                {
                    DateTime dt = (DateTime)oItem["Created"];
                    string formattedDate = string.Format("{0:dd MMM yyyy}", dt);
                    oSb.Append("<div class='panel panel-default faq-acordian'>");
                    oSb.Append("<div class='panel-heading'>");
                    oSb.Append("<a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapseRc" + i + "'>");
                    oSb.Append("<h4 class='panel-title'>");
                    oSb.Append("<i class='acc-con'>");
                    oSb.Append("<span class='glyphicon glyphicon-chevron-down'></span></i>");
                    oSb.Append("<p class='accHeadtxt'><span class='skyblue'>" + formattedDate + " </span>" + oItem["Title"] + "</p>");
                    oSb.Append("</h4>");
                    oSb.Append("</a>");
                    oSb.Append("</div>");
                    oSb.Append("<div id='collapseRc" + i + "' class='panel-collapse collapse in'>");
                    oSb.Append("<div class='panel-body acc-content'>");
                    oSb.Append("<p>" + oItem["Answer"] + "</p>");
                    oSb.Append("</div>");
                    oSb.Append("</div>");
                    oSb.Append("</div>");
                    i++;
                }
                mostRecentHTML = oSb.ToString();
            }
            catch (Exception ex)
            {
                mostRecentHTML = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GenerateRecentFAQHTML"), ex.StackTrace);
            }
            return mostRecentHTML;
        }

        /// <summary>
        /// Get FAQ's from all sites and populate in Global FAQ widget
        /// if Global site property is checked, then this function will execute
        /// </summary>
        /// <returns></returns>
        public static string GetGlobalFaqFromAllSites(string propListName, string propDisplayCount, string errMSG)
        {
            string mostRecent = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                mostRecent = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetRecentFAQFromCAMLQuery"), ex.StackTrace);
            }

            return mostRecent;
        }

        #endregion
        #region News and Announcement

        /// <summary>
        /// This method executes te search api to get most recent items from the current site
        /// </summary>
        /// <param name="clientContext"></param>
        /// <param name="propListName"></param>
        /// <param name="errMSG"></param>
        /// <returns></returns>
        public static string GetRecentNewsFromCAMLQuery(string propListName, string propDisplayCount, string errMSG)
        {
            string mostRecent = string.Empty;
            try
            {
                List oList = clientContext.Web.Lists.GetByTitle(propListName);
                CamlQuery camlQuery = new CamlQuery();
                camlQuery.ViewXml = "<View><Query><OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy></Query><RowLimit>" + propDisplayCount + "</RowLimit></View>";
                ListItemCollection items = oList.GetItems(camlQuery);
                clientContext.Load(items);
                clientContext.ExecuteQuery();
                mostRecent = GenerateRecentNewsHTML(items, errMSG);
            }
            catch (Exception ex)
            {
                mostRecent = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GetRecentNewsFromCAMLQuery"), ex.StackTrace);
            }

            return mostRecent;
        }

       
 /// <summary>
       ///  This method generates dynamic html based on the list item collection.
        /// </summary>
        /// <param name="clientcontext"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GenerateRecentNewsHTML(ListItemCollection oItems, string errMSG)
        {
            string mostRecentHTML = string.Empty;
            StringBuilder oSb = new StringBuilder();
            StringBuilder corosalimage = new StringBuilder();
            StringBuilder corosalimage2 = new StringBuilder();
            StringBuilder final = new StringBuilder();

            try
            {
                corosalimage.Append(@"<div id='carousel' class='carousel slide' data-ride='carousel'><ol class='carousel-indicators carousel-indicators-numbers'>");
                int ccount = 0, cnt = 0;
                corosalimage2.Append("<div class='carousel-inner' role='listbox'>");
               foreach (ListItem oItem in oItems)
                {
                    cnt = ccount + 1;
                    switch (ccount)
                    {
                        case 0:
                            corosalimage.Append("<li data-target='#carousel' data-slide-to='" + ccount + "' class='active'>" + cnt + "</li>");
                            break;
                        default:
                            corosalimage.Append("<li data-target='#carousel' data-slide-to='" + ccount + "' class=''>" + cnt + "</li>");
                            break;
                    }

                    switch (ccount)
                    {
                        case 0:
                            corosalimage2.Append("<div class='item active left'>");
                            break;
                        case 1:
                            corosalimage2.Append("<div class='item next left'>");
                            break;

                        default:
                            corosalimage2.Append("<div class='item'>");
                            break;
                    }
                    string sImageURL = string.Empty;
                   if(oItem["Image"]!=null)
                   { 
                    FieldUrlValue imgfield = (FieldUrlValue)oItem["Image"];
                   
                    if (imgfield != null)
                        sImageURL = imgfield.Url;
                   }
                   corosalimage2.Append("<img src='" + sImageURL + "' alt='slider-img'>");
                    corosalimage2.Append("<div class='carousel-caption'></div>");
                    corosalimage2.Append("<div class=''>");
                    string sTitleUrl = clientContext.Url + oItem["FileDirRef"] + "/DispForm.aspx?ID=" + oItem["ID"];
                    corosalimage2.Append("<a href='" + sTitleUrl + "'>");
                    //oSb.Append("<p class='skyblue'>" + oItem["Title"] + "</p>");
                    //oSb.Append("</a>");
                    corosalimage2.Append("<h5 class='slide-txt'>" + oItem["Title"] + "</h5></a>");
                    corosalimage2.Append("<span class='glyphicon glyphicon-time time-slide' aria-hidden='true'></span>");
                    DateTime dt = (DateTime)oItem["Created"];
                    string formattedDate = string.Format("{0:dd MMM yyyy}", dt);
                    corosalimage2.Append("<span class='icon-txt'>" + formattedDate + "</span>");
                    corosalimage2.Append("</div></div>");
                    
                    
                    ccount++;
                }

                corosalimage.Append("</ol>");
                //corosalimage2.Append("</div>");
                oSb.Append("</div></div>");
                final.Append(corosalimage);
                final.Append(corosalimage2);
                final.Append(oSb);

                mostRecentHTML = final.ToString();

            }
            catch (Exception ex)
            {
                mostRecentHTML = errMSG;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "CommonHelper", "GenerateRecentNewsHTML"), ex.StackTrace);
            }
            return mostRecentHTML;
        }

        #endregion
    }
}
