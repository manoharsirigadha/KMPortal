using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace IFDS.KMPortal.Webparts.FAQWebPart
{
    [ToolboxItemAttribute(false)]
    public class FAQWebPart : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/IFDS.KMPortal.WebParts/FAQWebPart/FAQWebPartUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }

        private const string DefaultWidgetTitle = "Global FAQ's";
        public static string faqWidgetTitle = DefaultWidgetTitle;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Widget Title"),
        WebDescription("Enter Widget Title"),
        DefaultValue(DefaultWidgetTitle)]

        public string _faqWidgetTitle
        {
            get { return faqWidgetTitle; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter title of the widget");
                faqWidgetTitle = value;
            }
        }

        private const string DefaultTab1 = "Most Popular";
        public static string faqTab1 = DefaultTab1;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Tab1 Name"),
        WebDescription("Enter name for Tab1"),
        DefaultValue(DefaultTab1)]

        public string _faqTab1
        {
            get { return faqTab1; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter name for tab1");
                faqTab1 = value;
            }
        }

        private const string DefaultTab2 = "Most Recent";
        public static string faqTab2 = DefaultTab2;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Tab2 Name"),
        WebDescription("Enter name for Tab2"),
        DefaultValue(DefaultTab2)]

        public string _faqTab2
        {
            get { return faqTab2; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter name for tab2");
                faqTab2 = value;
            }
        }

        private const string DefaultFAQListName = "FAQ";
        public static string faqListName = DefaultFAQListName;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("FAQ List Name"),
        WebDescription("FAQ List Properties"),
        DefaultValue(DefaultFAQListName)]

        public string _faqListName
        {
            get { return faqListName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter name of the list");
                faqListName = value;
            }
        }

        private const string DefaultFAQListCount = "3";
        public static string faqItemCount = DefaultFAQListCount;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("No of FAQ's to display"),
        WebDescription("Enter number of FAQ's to display"),
        DefaultValue(DefaultFAQListCount)]

        public string _faqItemCount
        {
            get { return faqItemCount; }
            set
            {
                if (string.IsNullOrEmpty(value) || value == "0")
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter number of items to display");
                faqItemCount = value;
            }
        }

        private const Boolean DefaultGlobalSite = false;
        public static Boolean faqGlobalSite = DefaultGlobalSite;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Global Site"),
        WebDescription("Enter True if it is global site else false for department site"),
        DefaultValue(DefaultGlobalSite)]

        public Boolean _faqGlobalSite
        {
            get { return faqGlobalSite; }
            set
            {
                faqGlobalSite = value;
            }
        }

        public static string faqSiteUrl;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Site URL"),
        WebDescription("Enter site url ")
        ]
        public string _faqSiteUrl
        {
            get { return faqSiteUrl; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Microsoft.SharePoint.WebPartPages.
                        WebPartPageUserException(
                        "Please enter site url");
                faqSiteUrl = value;
            }
        }

        private const string DefaultErrorMsg = "An error occurred while processing : please contact your administrator";
        public static string faqErrMsg = DefaultErrorMsg;
        [Category("FAQ Properties"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Error Message"),
        WebDescription("Display Error Message to appear in widget"),
        DefaultValue(DefaultErrorMsg)]

        public string _faqErrMsg
        {
            get { return faqErrMsg; }
            set
            {
                faqErrMsg = value;
            }
        }
    }
}
