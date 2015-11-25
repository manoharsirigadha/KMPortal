using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IFDS.KMPortal.Helper;
namespace IFDS.KMPortal.Webparts.NewsWebpart
{
    public partial class NewsWebpartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWidgetTitle.Text = NewsWebpart.NewsWidgetTitle;
          
            // lblTab2.Text = NewsAnnouncementWebpart.discTab2;

            bool isvalid = CommonHelper.GetClientContext(NewsWebpart.NewsListName, NewsWebpart.NewsItemCount, NewsWebpart.NewsSiteUrl);
            if (isvalid)
            {
                literalViewAll.Text = "<a href='" + CommonHelper.GetListUrlByTitle(NewsWebpart.NewsListName)[0] + "' class='more-globl' id='view-forum'>View All</a>";
                literalPost.Text = "<a href='" + CommonHelper.GetListUrlByTitle(NewsWebpart.NewsListName)[1] + "' class='post-globl'>Post</a>";
            }
        }

        /// <summary>
        /// This method queries list and retrieve most popular data.
        /// </summary>
        /// <returns></returns>
        /*  public static string GetMostPopularDiscussionData()
          {
              string mostPopularValue = string.Empty;
              try
              {
                  bool isvalid = CommonHelper.GetClientContext(DiscussionListWebPart.discListName, DiscussionListWebPart.discItemCount, DiscussionListWebPart.discSiteUrl);
                  if (isvalid)
                  {
                      mostPopularValue = CommonHelper.GetDiscussionsFromSearchAPI(DiscussionListWebPart.discListName, DiscussionListWebPart.discErrMsg);
                  }
                  else
                  {
                      mostPopularValue = DiscussionListWebPart.discErrMsg;
                  }
              }
              catch (Exception ex)
              {
                  mostPopularValue = DiscussionListWebPart.discErrMsg;
                  Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "DiscussionListWebPartUserControl", "GetMostPopularDiscussionData"), ex.StackTrace);
              }

              return mostPopularValue;
          }
          */
        /// <summary>
        /// This method queries list and retrieve most recent data.
        /// </summary>
        /// <returns></returns>
        public static string GetMostRecentNewsData()
        {
            string mostRecentValue = string.Empty;
            try
            {
                bool isvalid = CommonHelper.GetClientContext(NewsWebpart.NewsListName, NewsWebpart.NewsItemCount, NewsWebpart.NewsSiteUrl);
                if (isvalid)
                {
                    mostRecentValue = CommonHelper.GetRecentNewsFromCAMLQuery(NewsWebpart.NewsListName, NewsWebpart.NewsItemCount, NewsWebpart.NewsSiteUrl);
                }
                else
                {
                    mostRecentValue = NewsWebpart.NewsErrMsg;
                }
            }
            catch (Exception ex)
            {
                mostRecentValue = NewsWebpart.NewsErrMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "NewsAnnouncementWebpartUserControl", "GetMostRecentNewsData"), ex.StackTrace);
            }

            return mostRecentValue;

        }

    }
}
