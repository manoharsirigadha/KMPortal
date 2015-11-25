using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace IFDS.KMPortal.Webparts.NewsWebpart
{
    [ToolboxItemAttribute(false)]
    public class NewsWebpart : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/IFDS.KMPortal.Webparts/NewsWebpart/NewsWebpartUserControl.ascx";
        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }


        private const string DefaultWidgetTitle = "News/Events & Announcement";
        public static string NewsWidgetTitle = DefaultWidgetTitle;
        [Category("News Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Widget Title"),
        WebDescription("Enter Widget Title"),
        DefaultValue(DefaultWidgetTitle)]

        public string _NewsWidgetTitle
        {
            get { return NewsWidgetTitle; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter title of the widget");
                NewsWidgetTitle = value;
            }
        }

      
        private const string DefaultListName = "NewsAndAnnouncement";
        public static string NewsListName = DefaultListName;
        [Category("News Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("News List"),
        WebDescription("Enter List Name"),
        DefaultValue(DefaultListName)]

        public string _NewsListName
        {
            get { return NewsListName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter name of the list");
                NewsListName = value;
            }
        }



        private const string DefaultListCount = "3";
        public static string NewsItemCount = DefaultListCount;
        [Category("News Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("No of items to display"),
        WebDescription("Enter number of items to display"),
        DefaultValue(DefaultListCount)]

        public string _NewsItemCount
        {
            get { return NewsItemCount; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter number of items to display");
                NewsItemCount = value;
            }
        }

        private const string DefaultSiteUrl = "http://cad2ca1vspw05/";
        public static string NewsSiteUrl = "";
        [Category("News Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Site URL"),
        WebDescription("Enter site url "),
         DefaultValue(DefaultSiteUrl)]
        public string _NewsSiteUrl
        {
            get { return NewsSiteUrl; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter site url");
                NewsSiteUrl = value;
            }
        }



        private const string DefaultErrorMsg = "An error occurred while processing : please contact your administrator";
        public static string NewsErrMsg = DefaultErrorMsg;
        [Category("News Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Error Message"),
        WebDescription("Display Error Message to appear in widget"),
        DefaultValue(DefaultErrorMsg)]

        public string _NewsErrMsg
        {
            get { return NewsErrMsg; }
            set
            {
                NewsErrMsg = value;
            }
        }


    }





}
