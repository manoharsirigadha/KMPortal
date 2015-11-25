<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiscussionListWebPartUserControl.ascx.cs" Inherits="IFDS.KMPortal.Webparts.DiscussionListWebPart.DiscussionListWebPartUserControl" %>


<%--<div class="wraper">
    <div class="container main-container">
        <!--Main content starts-->

        <div class="container main-global">
            <div id="home">--%>

                <!--2nd panel starts-->
        
                    <div class="panel-heading ">
                        <h3 class="panel-title">
                            <div class="sub-caption">
                             <span  class="forumicon"></span>
                                <asp:Label ID="lblWidgetTitle" runat="server"></asp:Label>
  <a class="help-icon" href="../Global IFDS/help.html#forumglobal" onclick="window.open(this.href, 'mywin',

'left=300,top=20,width=800,height=600, margin=0 auto,toolbar=1,resizable=0'); return false;">

      </a>

                                
                                <asp:Literal ID="literalViewAll" runat="server"></asp:Literal>
                                <asp:Literal ID="literalPost" runat="server"></asp:Literal>
                               <%-- <a href="newdiscus.html" class="post-globl">Post</a>--%>

                            </div>
                        </h3>

                    </div>
		<div class="">

	<ul class="nav nav-tabs tb-border">
	  <li class="active"><a data-toggle="tab" href="#Popular"><asp:Label ID="lblTab1" runat="server"></asp:Label></a></li>
		<li><a data-toggle="tab" href="#Recent"><asp:Label ID="lblTab2" runat="server"></asp:Label></a></li>
	</ul>


                         <div class="tab-content">

                            <div id='Popular' class='tab-pane fade active in'>
                                <div class=" outer forum-scroll">
                                <div class='scrolldiv forumdiv' id="MostPopular" >
                                     </div>

                                </div>
                            </div>
                                
                            <div id='Recent' class='tab-pane fade'>
                                <div class=" outer forum-scroll">
                                <div class='crolldiv forumdiv' id="MostRecent">
                                     </div>
                                </div>
                            </div>
                            
                        </div>

                    </div>

    



<%--            </div>

        </div>

    </div>
</div>--%>
<!--wraper ends -->

<!-- JavaScript-->
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
                url: "/_Layouts/15/IFDS.KMPortal/WebMethodPage.aspx/MostPopularData",
                success: function (msg) {
                    $("#MostPopular").html(msg.d);

                },
                error: function (msg) {
                    alert('error' + msg);
                }
            });

            $('a[href="#Popular"]').click(function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: '{ "p1":' + pVal + ' }',
                    url: "/_Layouts/15/IFDS.KMPortal/WebMethodPage.aspx/MostPopularData",
                    success: function (msg) {
                        $("#MostPopular").html(msg.d);

                    },
                    error: function (msg) {
                        alert('error' + msg);
                    }
                });
            });

            $('a[href="#Recent"]').click(function () {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //data: '{ "p1":' + pVal + ' }',
                    url: "/_Layouts/15/IFDS.KMPortal/WebMethodPage.aspx/MostRecentData",
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

