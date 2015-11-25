using IFDS.KMPortal.Helper;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace IFDS.KMPortal.Webparts.DiscussionListWebPart
{
    public partial class DiscussionListWebPartUserControl : UserControl
    {
        /// <summary>
        /// This method executes when the page loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblWidgetTitle.Text = DiscussionListWebPart.discWidgetTitle;
            lblTab1.Text = DiscussionListWebPart.discTab1;
            lblTab2.Text = DiscussionListWebPart.discTab2;

            bool isvalid = CommonHelper.GetClientContext(DiscussionListWebPart.discListName, DiscussionListWebPart.discItemCount, DiscussionListWebPart.discSiteUrl);
            if (isvalid)
            {
                literalViewAll.Text = "<a href='" + CommonHelper.GetListUrlByTitle(DiscussionListWebPart.discListName)[0] + "' class='more-globl' id='view-forum'>View All</a>";
                literalPost.Text = "<a href='" + CommonHelper.GetListUrlByTitle(DiscussionListWebPart.discListName)[1] + "' class='post-globl'>Post</a>";
            }
        }

        /// <summary>
        /// This method queries list and retrieve most popular data.
        /// </summary>
        /// <returns></returns>
        public static string GetMostPopularDiscussionData()
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

        /// <summary>
        /// This method queries list and retrieve most recent data.
        /// </summary>
        /// <returns></returns>
        public static string GetMostRecentDiscussionData()
        {
            string mostRecentValue = string.Empty;
            try
            {
                bool isvalid = CommonHelper.GetClientContext(DiscussionListWebPart.discListName, DiscussionListWebPart.discItemCount, DiscussionListWebPart.discSiteUrl);
                if (isvalid)
                {
                    mostRecentValue = CommonHelper.GetRecentDiscussionsFromCAMLQuery(DiscussionListWebPart.discListName, DiscussionListWebPart.discItemCount, DiscussionListWebPart.discErrMsg);
                }
                else
                {
                    mostRecentValue = DiscussionListWebPart.discErrMsg;
                }
            }
            catch (Exception ex)
            {
                mostRecentValue = DiscussionListWebPart.discErrMsg;
                Logger.WriteLog(Logger.Category.Unexpected, string.Format("{0}-{1}", "DiscussionListWebPartUserControl", "GetMostRecentDiscussionData"), ex.StackTrace);
            }

            return mostRecentValue;

        }

    }
}
