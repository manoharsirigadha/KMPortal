using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.Services;
using IFDS.KMPortal.Webparts.DiscussionListWebPart;
using IFDS.KMPortal.Webparts.FAQWebPart;
using IFDS.KMPortal.Webparts.NewsWebpart; 

namespace IFDS.KMPortal.Layouts.IFDS.KMPortal
{
    public partial class WebMethodPage : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Get Most Popular Data from Discussion visual webpart
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string MostPopularData()
        {
            string mostPopular = DiscussionListWebPartUserControl.GetMostPopularDiscussionData();
            return mostPopular;
        }

        /// <summary>
        /// Get Most Recent Data from Discussion visual webpart
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string MostRecentData()
        {
            string mostRecent = DiscussionListWebPartUserControl.GetMostRecentDiscussionData();
            return mostRecent;
        }

        /// <summary>
        /// Get Most Popular Data from FAQ visual webpart
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string FAQMostPopularData()
        {
            string mostPopularFAQ = string.Empty;
            if (FAQWebPart.faqGlobalSite == true)
            {
                mostPopularFAQ = FAQWebPartUserControl.GetMostPopularFAQForGlobalSites();
            }
            else
            {
                mostPopularFAQ = FAQWebPartUserControl.GetMostPopularFAQData();
            }

            return mostPopularFAQ;
        }

        /// <summary>
        /// Get Most Popular Data from FAQ visual webpart based on the condition if user select's global or departmental site
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string FAQMostRecentData()
        {
            string mostRecentFAQ = string.Empty;
            if (FAQWebPart.faqGlobalSite == true)
            {
                mostRecentFAQ = FAQWebPartUserControl.GetMostRecentFAQForGlobalSites();
            }
            else
            {
                mostRecentFAQ = FAQWebPartUserControl.GetMostRecentFAQData();
            }
            return mostRecentFAQ;
        }

        /// <summary>
        /// Get Most Recent Data from News and Announcement visual webpart
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string MostRecentNews()
        {
            string mostRecent = NewsWebpartUserControl.GetMostRecentNewsData();
            return mostRecent;
        }


    }
}
