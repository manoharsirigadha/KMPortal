<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FAQWebPartUserControl.ascx.cs" Inherits="IFDS.KMPortal.Webparts.FAQWebPart.FAQWebPartUserControl" %>
<%--<div class="wraper">
    <div class="container main-container">
        <!--Main content starts-->

        <div class="container main-global">
            <div id="home">--%>

                <!--2nd panel starts-->
                <div class="panel panel-default subcontentbox panel4">
                    <div class="panel-heading ">
                        <h3 class="panel-title">
                            <div class="sub-caption">
                                <span class="faqicon"></span>
                                <asp:Label ID="lblWidgetTitle" runat="server"></asp:Label> 
      <a class="help-icon" href="../Global IFDS/help.html#faqglobal" onclick="window.open(this.href, 'mywin',
'left=300,top=20,width=800,height=600, margin=0 auto,toolbar=1,resizable=0'); return false;">
          </a>

                                
                                <asp:Literal ID="literalViewAll" runat="server"></asp:Literal>
                                <asp:Literal ID="literalPost" runat="server"></asp:Literal>
                                <%--<a href="newdiscus.html" class="post-globl">Post</a>--%>
                            </div>
                        </h3>
                    </div>
                    <div >
                        <ul class="nav nav-tabs tb-border">
                            <li class="active"><a data-toggle="tab" href="#PopularFaq" aria-expanded="true"><asp:Label ID="lblTab1" runat="server"></asp:Label></a></li>
                            <li class=""><a data-toggle="tab" href="#RecentFaq" aria-expanded="false"><asp:Label ID="lblTab2" runat="server"></asp:Label></a></li>
                        </ul>

                        <div class="tab-content">
                            <div id="PopularFaq" class="tab-pane fade in active">
                                <div class="outer faq-scroll">
                                    <div class="scrolldiv " >
                                            <div class="panel-group" id="accordion">
                                                <div id="MostPopular">
                                                </div>
                                            </div>
                                        </div>
                                        <div id="mCSB_6_scrollbar_vertical" class="mCSB_scrollTools mCSB_6_scrollbar mCS-inset mCSB_scrollTools_vertical" style="display: block;"><a href="#" class="mCSB_buttonUp" oncontextmenu="return false;" style="display: block;"></a>
                                            <div class="mCSB_draggerContainer">
                                                <div id="mCSB_6_dragger_vertical" class="mCSB_dragger" style="position: absolute; min-height: 30px; display: block; height: 263px; max-height: 265px; top: 0px;" oncontextmenu="return false;">
                                                    <div class="mCSB_dragger_bar" style="line-height: 30px;"></div>
                                                </div>
                                                <div class="mCSB_draggerRail"></div>
                                            </div>
                                            <a href="#" class="mCSB_buttonDown" oncontextmenu="return false;" style="display: block;"></a></div>
                                    </div>
                                </div>
                            </div>

                            <div id="RecentFaq" class="tab-pane fade">
                                <div class="col-lg-12 outer faq-scroll mCustomScrollbar _mCS_7 mCS_no_scrollbar">
                                    <div id="mCSB_7" class="mCustomScrollBox mCS-inset mCSB_vertical mCSB_inside" tabindex="0">
                                        <div id="mCSB_7_container" class="mCSB_container mCS_y_hidden mCS_no_scrollbar_y" style="position: relative; top: 0; left: 0;" dir="ltr">


                                            <div class="panel-group" id="accordion">
                                                <div id="MostRecent">
                                                </div>
                                            </div>
                                        </div>
                                        <div id="mCSB_7_scrollbar_vertical" class="mCSB_scrollTools mCSB_7_scrollbar mCS-inset mCSB_scrollTools_vertical" style="display: none;"><a href="#" class="mCSB_buttonUp" oncontextmenu="return false;" style="display: block;"></a>
                                            <div class="mCSB_draggerContainer">
                                                <div id="mCSB_7_dragger_vertical" class="mCSB_dragger" style="position: absolute; min-height: 30px; top: 0px; display: block; height: 263px; max-height: 265px;" oncontextmenu="return false;">
                                                    <div class="mCSB_dragger_bar" style="line-height: 30px;"></div>
                                                </div>
                                                <div class="mCSB_draggerRail"></div>
                                            </div>
                                            <a href="#" class="mCSB_buttonDown" oncontextmenu="return false;" style="display: block;"></a></div>
                                    </div>
                                </div>
                            </div>



                        </div>
                    </div>
                </div>



            <%--</div>

        </div>

    </div>
</div>--%>
<!--wraper ends -->
<script src="/_layouts/15/KM/js/jquery-1.11.1.js"></script>

<script src="/_layouts/15/KM/js/bootstrap.min.js"></script>

<script src="/_layouts/15/KM/js/global.js"></script>

<script src="/_layouts/15/KM/js/dropdownhover.min.js"></script>

<script src="/_layouts/15/KM/js/vmenuModule.js"></script>

<script src="/_layouts/15/KM/js/jquery.mCustomScrollbar.concat.min.js"></script>
<script>
    jQuery.noConflict();
    (function ($) {

        $(window).load(function () {

            $.mCustomScrollbar.defaults.theme = "inset"; //set "inset" as the default theme

            $.mCustomScrollbar.defaults.scrollButtons.enable = true; //enable scrolling buttons by default

            $(".outer,.nested").mCustomScrollbar();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //data: '{ "p1":' + pVal + ' }',
                url: "/_Layouts/15/IFDS.KMPortal/WebMethodPage.aspx/FAQMostPopularData",
                success: function (msg) {
                    $("#MostPopular").html(msg.d);
                },
                error: function (msg) {
                    alert('error' + msg);
                }
            });

            $('a[href="#PopularFaq"]').click(function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: '{ "p1":' + pVal + ' }',
                    url: "/_Layouts/15/IFDS.KMPortal/WebMethodPage.aspx/FAQMostPopularData",
                    success: function (msg) {
                        $("#MostPopular").html(msg.d);
                    },
                    error: function (msg) {
                        alert('error' + msg);
                    }
                });
            });

            $('a[href="#RecentFaq"]').click(function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: '{ "p1":' + pVal + ' }',
                    url: "/_Layouts/15/IFDS.KMPortal/WebMethodPage.aspx/FAQMostRecentData",
                    success: function (msg) {
                        $("#MostRecent").html(msg.d);
                    },
                    error: function (msg) {
                        alert('error' + msg);
                    }
                });

            });

        });

    })(jQuery);



</script>

