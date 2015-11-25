
/*---------------------------------------------------------------------------------
* IFDS.KMPortal.Notification.js file.
* Description : This Javascript file reads news feeds and display that has notification
                in KM Portal Master Page. 
* DEPENDENCIES
         
	     *  - Jquery-1.11.1.js   
         *  - jquery-ui.js
         *  - jquery.SPServices2013.01.min.js
         

         *  - Author: Madhukumar G.K

         * Created: September November 6th 2015
*------------------------------------------------------------------------------*/

var notification = (function ($) {

    'use strict'

    $.support.cors = true;

    /**
    * Global variables used within the form.
    *
    * @version 1.0.0
    */

    var currentUserName = '', currentUserId = '', webUrl = '', notifyHostWebUrl = '', web = '',
    latestTimeStamp = '', userProfileTimeStamp = '', scriptbase = '', myNotificationIntervalVar = '',
    user = '', context = '', mySiteUrl ='',

     /**
	 * Updates custom user profile property (notifcationtimestamp) with latesttime stamp fom feed.
	 *
	 * @version 1.0.0
	 */
    updateTimeStampUserProp = function () {

        if (latestTimeStamp !== '')
            {
        var propertyName = "notificationtimestamp";
        var propertyValue = latestTimeStamp;
        var targetUser = currentUserId.split("|")[1];
        updateUserProfile(targetUser, propertyName, propertyValue);
           }
    },

    /**
	 * Updates custom user profile property (notifcationtimestamp) with latesttime stamp fom feed.
	 *
	 * @version 1.0.0
    */
    updateUserProfile = function (userId, propertyName, propertyValue) {
        var propertyData = "<PropertyData>" +
        "<IsPrivacyChanged>false</IsPrivacyChanged>" +
        "<IsValueChanged>true</IsValueChanged>" +
        "<Name>" + propertyName + "</Name>" +
        "<Privacy>NotSet</Privacy>" +
        "<Values><ValueData><Value xsi:type=\"xsd:string\">" + propertyValue + "</Value></ValueData></Values>" +
        "</PropertyData>";

        $().SPServices({
            operation: "ModifyUserPropertyByAccountName",
            async: false,
            webURL: "/",
            accountName: userId,
            newData: propertyData,
            completefunc: function (xData, Status) {
                var result = $(xData.responseXML);
            }
        });
    },

    /**
	 * Get news feeds of the current logged in user.
	 *
	 * @version 1.0.0
	 */
    getMyFeeds = function () {

        //alert(mySiteUrl);
        //$('a#lnkView').prop('href', mySiteUrl);

        var t = '';

        $.ajax({
            url: webUrl + "/_api/social.feed/my/feed",
            method: "GET",
            headers: { "Accept": "application/json; odata=verbose" },
            dataType: 'json',
            success: function (data) {
                var count = 0;
                if (data.d.SocialFeed.UnreadMentionCount !== null) {

                    if (data.d.SocialFeed.Threads.results.length !== 0) {

                        latestTimeStamp = data.d.SocialFeed.Threads.results[0].RootPost.CreatedTime;
                        $.each(data.d.SocialFeed.Threads.results, function (i, item) {
                            var checkTimeStamp = item.RootPost.CreatedTime;

                            /* userProfileTimeStamp = gets notificationtimestampp from user profile properties */

                            if (checkTimeStamp > userProfileTimeStamp) {
                                count++;
                                $('#notificationCount').text(count);
                                if (i < 4) {
                                    var postCreatedTime = $.datepicker.formatDate('dd M yy', new Date(item.RootPost.CreatedTime));
                                    t = t + "<a class='' href='#'>";
                                    t = t + "<div class='notification-item'>";
                                    t = t + "<h5 class='item-title' >" + item.RootPost.Text + "</h5>";
                                    t = t + "<span class='glyphicon glyphicon-time notify-time' aria-hidden='true'></span>";
                                    t = t + "<span class='notytime-txt'>" + postCreatedTime + "</span>";
                                    t = t + "</div>";
                                    t = t + "<li class='divider'></li>";
                                    t = t + "</a>";
                                }
                            }
                        });
                        if (count == 0) {
                            $('#notificationCount').text("0");
                            $('#mCSB_1_container').append("No posts available");
                        }
                        $('#mCSB_1_container').append(t);
                    }

                    if (count == 0) {
                        $('#notificationCount').text("0");
                        $('#mCSB_1_container').append("No posts available");
                    }
                }
            }



            , error: function (sender, args) {
                alert("Failed in getting my feeds" + args);
            }
        });
    },

    /**
	 * Get custom user profile properties (notificationtimestamp) of the current logged in user.
	 *
	 * @version 1.0.0
	 */
    getUserProfileProperties = function () {
        var user = {};
        $().SPServices({
            operation: 'GetUserProfileByName',
            async: false,
            completefunc: function (xData, Status) {
                $(xData.responseXML).SPFilterNode("PropertyData").each(function () {
                    user[$(this).find("Name").text()] = $(this).find("Value").text();
                });
                userProfileTimeStamp = user.notificationtimestamp;
                //mySiteUrl = user.PublicUrl;
                /* Retrieve News Feed from mysite */
                getMyFeeds();
                /**
	             * These properties can be used if required.
	             *
	             * @version 1.0.0
	             */
                //user.login = user.AccountName;
                //user.full_name = user.PreferredName;
                //user.email = user.WorkEmail;
            }
        });

    },

    /**
	 * Get current user  Login name and title from the current client context .
	 *
	 * @version 1.0.0
	 */
    getCurrentUser = function () {
        context = new SP.ClientContext.get_current();
        web = context.get_web();
        user = web.get_currentUser();
        context.load(user);
        context.executeQueryAsync(Function.createDelegate(this, checkUserSucceeded), Function.createDelegate(this, checkUserFailed));
    },
    /**
	 * Get current user  Login name and title from the current client context .
	 *
	 * @version 1.0.0
	 */
    checkUserSucceeded = function () {
        currentUserName = user.get_title();
        currentUserId = user.get_loginName();
        $.getScript(scriptbase + "SP.UserProfiles.js", getUserProfileProperties);
    },

    checkUserFailed = function (sender, args) {
        alert("Failed in get current user method" + args);
    },

    /**
	 * Get current user on frequent intervals .
	 *
	 * @version 1.0.0
	 */

    myNotificationTimer = function () {
        ExecuteOrDelayUntilScriptLoaded(getCurrentUser, "sp.js");
    },

    /**
	 * Bind all events associated with the form.
	 *
	 * @version 1.0.0
	 */

    bindEvents = function () {

       scriptbase = notifyHostWebUrl + "/_layouts/15/";

      //myNotificationIntervalVar = setInterval(function () { myNotificationTimer() }, 30000);
      // myNotificationIntervalVar = function () { myNotificationTimer() }, 30000);
        /**
        * Get absolute web url of the site.
        *
        * @version 1.0.0
        */
        webUrl = _spPageContextInfo.siteAbsoluteUrl;
        SP.SOD.executeFunc('sp.js', 'SP.ClientContext', getCurrentUser);
       // ExecuteOrDelayUntilScriptLoaded(getCurrentUser, "sp.js");

        /**
        * Fire event on save button click and perfroms the required operation.
        *
        * @version 1.0.0
        */

        $("#btnNotify").click(function () {
            SP.SOD.executeOrDelayUntilScriptLoaded(updateTimeStampUserProp, 'SP.UserProfiles.js');
        });


    },

    /**
	 * Initiate the bind function on page load.
	 *
	 * @version 1.0.0
	 */

    init = function () {
        bindEvents();
    },

    /**
	 * Fire events on document ready, and bind other events.
	 *
	 * @version 1.0.0
	 */
    ready = function () {
        init();
    };

    // Only expose the ready function to the world
    return {
        ready: ready
    };

})($);

$(notification.ready);



