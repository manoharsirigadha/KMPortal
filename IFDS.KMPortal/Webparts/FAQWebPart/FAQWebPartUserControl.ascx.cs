using IFDS.KMPortal.Helper;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace IFDS.KMPortal.Webparts.FAQWebPart
{
    public partial class FAQWebPartUserControl : UserControl
    {
        /// <summary>
        /// This method executes when the page loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWidgetTitle.Text = FAQWebPart.faqWidgetTitle;
            lblTab1.Text = FAQWebPart.faqTab1;
            lblTab2.Text = FAQWebPart.faqTab2;

            bool isvalid = CommonHelper.GetClientContext(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqSiteUrl);
            if (isvalid)
            {
                literalViewAll.Text = "<a href='" + CommonHelper.GetListUrlByTitle(FAQWebPart.faqListName)[0] + "' class='more-globl' id='view-forum'>View All</a>";
                literalPost.Text = "<a href='" + CommonHelper.GetListUrlByTitle(FAQWebPart.faqListName)[1] + "' class='post-globl'>Post</a>";
            }
        }

        /// <summary>
        /// This method queries list and retrieve most popular data.
        /// </summary>
        /// <returns></returns>
        public static string GetMostPopularFAQData()
        {
            string mostPopularValue = string.Empty;
            try
            {
                bool isvalid = CommonHelper.GetClientContext(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqSiteUrl);
                if (isvalid)
                {
                    mostPopularValue = CommonHelper.GetFAQFromSearchAPI(FAQWebPart.faqListName, FAQWebPart.faqErrMsg);
                }
                else
                {
                    mostPopularValue = FAQWebPart.faqErrMsg;
                }
            }
            catch (Exception ex)
            {
                mostPopularValue = FAQWebPart.faqErrMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "FAQWebPartUserControl", "GetMostPopularFAQData"), ex.StackTrace);
            }

            return mostPopularValue;
        }

        /// <summary>
        /// This method queries list and retrieve most recent data.
        /// </summary>
        /// <returns></returns>
        public static string GetMostRecentFAQData()
        {
            string mostRecentValue = string.Empty;
            try
            {
                bool isvalid = CommonHelper.GetClientContext(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqSiteUrl);
                if (isvalid)
                {
                    mostRecentValue = CommonHelper.GetRecentFAQFromCAMLQuery(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqErrMsg);
                }
                else
                {
                    mostRecentValue = FAQWebPart.faqErrMsg;
                }
            }
            catch (Exception ex)
            {
                mostRecentValue = FAQWebPart.faqErrMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "FAQWebPartUserControl", "GetMostRecentFAQData"), ex.StackTrace);
            }

            return mostRecentValue;

        }

        /// <summary>
        /// This method queries list and retrieve most popular data.
        /// </summary>
        /// <returns></returns>
        public static string GetMostPopularFAQForGlobalSites()
        {
            string mostRecentValue = string.Empty;
            try
            {
                bool isvalid = CommonHelper.GetClientContext(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqSiteUrl);
                if (isvalid)
                {
                    mostRecentValue = CommonHelper.GetGlobalPopularFaq(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqErrMsg);
                }
                else
                {
                    mostRecentValue = FAQWebPart.faqErrMsg;
                }
            }
            catch (Exception ex)
            {
                mostRecentValue = FAQWebPart.faqErrMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "FAQWebPartUserControl", "GetMostRecentFAQData"), ex.StackTrace);
            }

            return mostRecentValue;

        }

        /// <summary>
        /// This method queries list and retrieve most recent data.
        /// </summary>
        /// <returns></returns>
        public static string GetMostRecentFAQForGlobalSites()
        {
            string mostRecentValue = string.Empty;
            try
            {
                bool isvalid = CommonHelper.GetClientContext(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqSiteUrl);
                if (isvalid)
                {
                    mostRecentValue = CommonHelper.GetGlobalFaqFromAllSites(FAQWebPart.faqListName, FAQWebPart.faqItemCount, FAQWebPart.faqErrMsg);
                }
                else
                {
                    mostRecentValue = FAQWebPart.faqErrMsg;
                }
            }
            catch (Exception ex)
            {
                mostRecentValue = FAQWebPart.faqErrMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "FAQWebPartUserControl", "GetMostRecentFAQData"), ex.StackTrace);
            }

            return mostRecentValue;

        }

    }
}
