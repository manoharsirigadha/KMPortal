
$(document).ready(function(){
// faq collapse
$('.collapse').on('shown.bs.collapse', function(){
$(this).parent().find(".glyphicon-chevron-right").removeClass("glyphicon-chevron-right").addClass("glyphicon-chevron-down");
}).on('hidden.bs.collapse', function(){
$(this).parent().find(".glyphicon-chevron-down").removeClass(" glyphicon-chevron-down").addClass("glyphicon-chevron-right");
});
// end faq collaps


//my favs viewall
$("#view-news").click(function(){
    $("#home").hide();
    $("#favAll").show();
});
//discussion forum view all

$("#view-forum").click(function(){
    $("#home").hide();
    $("#discussAll").show();
});

//top contibuters view all
$("#view-topcontributer").click(function(){
   $("#home").hide();
    $("#top-contributer").show();
}); 

//Faqs view all

$("#view-faqs").click(function(){
    $("#home").hide();
    $("#faqsAll").show();
});

$("#search").click(function(){
    $("#searchResult").hide();
    $("#resultPage").show();
});
$("#BusinessSearch a").click(function(){
    $("#searchResult").hide();
    $("#resultPage").show();
});

	$(".u-vmenu").vmenuModule({
					Speed: 100,
					autostart: false,
					autohide: true
				});
				
/*Tab pane*/

 // $('body').on('click','[data-toggle="tab"]',function(){
 // var id= $(this).attr('href');
 // var actual = id.substring(1)
     // $("div[id=actual]").find(".scrolldiv").getNiceScroll().hide();
                  // $($(this).attr('href')).find(".scrolldiv").getNiceScroll().show();
 // });
	 
	  // $(".scrolldiv,.scrolldivnot").niceScroll({
			// cursorcolor: "#000",
			// autohidemode: false,
			// background: "#ddd",
			// cursorfixedheight: 60,
			// //cursoropacitymin:1,
			 // cursorwidth: "7px",
			 // horizrailenabled: false
			// });
			
			

// $('.left-menubox >ul').niceScroll({cursorcolor: "#000",
			// autohidemode: false,
			// background: "#ddd",
			// cursorfixedheight: 60,
			// //cursoropacitymin:1,
			 // cursorwidth: "7px",});
		// //	$(".scrolldiv").scrollTop(0)
			// //scrollbar();
			// // $('.left-menubox i').click(function(e)
			// // {
			// // scrollbar();
			// // });
			
			 document.onselectstart=new Function('return false')
			 if(window.sidebar)
			 {
			 document.onmousedown=killCopy	
			 document.onclick=reEnable
			 }
			
			
			
 });



(function($) {
	$.fn.vmenuModule = function(option) {
		var obj,
			item;
		var options = $.extend({
				Speed: 100,
				autostart: true,
				autohide: 0
			},
			option);
		obj = $(this);

		item = obj.find("ul").parent("li").children("i");
		item.attr("data-option", "off");

		item.unbind('click').on("click", function() {
			var i = $(this);
			if (options.autohide) {
				i.parent().parent().find("i[data-option='on']").parent("li").children("ul").slideUp(options.Speed / 1.2,
					function() {
						$(this).parent("li").children("i").attr("data-option", "off");
						 leftscroll();
					})
			}
			if (i.attr("data-option") == "off") {
				var plft=parseInt( i.prev().css('padding-left'),10);
				
				i.parent("li").children("ul").find('a').each(function(){
				$(this).css('padding-left',plft+10 +'px');
				var lft=parseInt( i.css('left'),10);
				$(this).next('i').css('left',lft+10+'px');
				});
				i.parent("li").children("ul").slideDown(options.Speed,
					function() {
						i.attr("data-option", "on");
						 leftscroll();
					});
			}
			if (i.attr("data-option") == "on") {
				i.attr("data-option", "off");
				i.parent("li").children("ul").slideUp(options.Speed)
			}
		});
		if (options.autostart) {
			obj.find("i").each(function() {

				$(this).parent("li").parent("ul").slideDown(options.Speed,
					function() {
						$(this).parent("li").children("i").attr("data-option", "on");
						 leftscroll();
					})
			})
		}

	}
})(window.jQuery || window.Zepto);
// function scrollbar()
// {
 // $('.left-menubox').each(function(){
	
		// var actual =$(this).height()-$(this).find('.backcolor').height();;
		
		
		// console.log(actual);
		// if(actual>=310){
		// $(this).niceScroll({
			// cursorcolor: "#000",
			// autohidemode: false,
			// background: "#ddd",
			// cursorfixedheight: 60,
			// //cursoropacitymin:1,
			 // cursorwidth: "7px"
			// });
		// }
		
	  // });
	  // }

// $(document).ready(function() {
			
				
				
	
			// });
			
			
	$(window).load(function(){
        
         $.mCustomScrollbar.defaults.theme="light-3"; //set "inset" as the default theme
         $.mCustomScrollbar.defaults.scrollButtons.enable=true; //enable scrolling buttons by default
       
     
          $(".scrolldiv,.scrolldivnot").mCustomScrollbar();
		  leftscroll();
           
    });	
function leftscroll(){
 var mAx =10;
		$('.left-menubox').each(function(){
		 
		mAx =mAx+ $(this).height();
		 });
		// alert(mAx);
		 if(mAx>543)
		 {
		 
		 $('.left-menubox').each(function(){
			$(this).find('ul.ulnav').mCustomScrollbar();
		 });
		 }
		 }	
			function killCopy()
			{
			return false
			}
			function reEnable()
			{
			return true
			}
			