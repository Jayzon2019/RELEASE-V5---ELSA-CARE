$(document).ready(function() {
	$('.hero-slider a[href*="#"]:not([href="#"])').click(function() {
		if (location.pathname.replace(/^\//, "") == this.pathname.replace(/^\//, "")
				&& location.hostname == this.hostname)
			{
				var target = $(this.hash);
				target = target.length ? target : $("[name=" + this.hash.slice(1) + "]");
				if (target.length) {
					$("html, body").animate(
						{
							scrollTop: target.offset().top
						},
						1000
					);
					return false;
				}
			}
	});

	var windowSize = $(window).width();

	if(windowSize <= 2560 && windowSize >= 1921) {
		$('.hero-banner').attr('src', 'assets/images/2560px.jpg');
		$('.section-hero').css('background-image', 'url(assets/images/2560px.jpg)');
	}
	else if (windowSize <= 1920 && windowSize >= 1441) {
		$('.hero-banner').attr('src', 'assets/images/1920px.jpg');
		$('.section-hero').css('background-image', 'url(assets/images/prime-care-1920.jpg)');
	}
	else if (windowSize <= 1440 && windowSize >= 1367) {
		$('.hero-banner').attr('src', 'assets/images/1440px.jpg');
		$('.section-hero').css('background-image', 'url(assets/images/1440px.jpg)');
	}
	else if (windowSize <= 1366 && windowSize >= 1281) {
		$('.hero-banner').attr('src', 'assets/images/1366px.jpg');
		$('.section-hero').css('background-image', 'url(assets/images/1366px.jpg)');
	}
	else if (windowSize <= 1280 && windowSize >= 1024) {
		$('.hero-banner').attr('src', 'assets/images/1280px.jpg');
		$('.section-hero').css('background-image', 'url(assets/images/1280px.jpg)');
	}
	else if (windowSize <= 1023) {
		$('.hero-banner').attr('src', 'assets/images/prime-care-1920.jpg');
		$('.section-hero').css('background-image', 'none !important');
	}

	$(".hero-slider").slick({
		dots: true,
		arrows: false,
		autoplay: true,
		adaptiveHeight: true,
		autoplaySpeed: 5000
	});

	var windowWidth = $(window).innerWidth();
 	new WOW().init(); //scroll animation

	// modal options
	$.modal.defaults = {
		showClose: false,
		clickClose: true
	};

	$("[data-buy-link]").on("click", e => {
		const buyLink = $(e.target).data("buy-link");
		const proceedButton = $("[data-proceed-button]");
		proceedButton.attr("href", buyLink);
	});

	$(".burger-menu").on("click", function(e) {
		e.preventDefault();
		$(this).toggleClass("active");
		$(".main-nav").toggleClass("show");
	});

	// Cookie consent
	// memo
	var $memo = $(".memo");
	var cookieConsentKey = "has-cookie-consent";

	$(".memo-close").on("click", e => {
		e.preventDefault();

		$memo.hide();

		var expiration = new Date();
		expiration = expiration.setDate(expiration.getDate() + 90);

		Cookie.set(cookieConsentKey, true, expiration);
	});

	var hasCookieConsent = (Cookie ? Cookie.get(cookieConsentKey) : false);

	if (!hasCookieConsent) {
		$memo.show();
	}


	$(".modal").on($.modal.OPEN, function(event, modal) {
		if (windowWidth <= 767) {
			const buttonHeight = $(this)
			.find(".modal-footer")
			.outerHeight();
			$(this)
			.find(".modal-body")
			.css("padding-bottom", buttonHeight + "px");
		}
	});

	// load more
	$(".product-list li")
		.slice(0, 4)
		.show();

	$("#loadmore").on("click", function(e) {
		e.preventDefault();
		const offset =
		$(".product-list").offset().top + $(".product-list").outerHeight();
		$(".product-list li:hidden")
		.slice(0, 4)
		.slideDown();

		if ($(".product-list li:hidden").length == 0) {
			$("#loadmore").fadeOut("slow");
	  		// $("#loadmore").text("Show Less");
		}

		console.log($('.product-list li:hidden').length);
		$("html,body").animate(
			{
				scrollTop: offset
			},
			1500
		);
	});

	$(".preloader .preloader-content").addClass("content-wrapper");
});



window.onload = function() {
	$(".preloader .preloader-content")
		.delay(2000)
		.fadeOut("slow");

	$(".preloader")
		.delay(2000)
		.fadeOut("slow");
};



// Initial state
var scrollPos = 0;
var header = document.querySelector("header");
var mobile = $(window).width();

// adding scroll event
window.addEventListener("scroll", function() {

	if (mobile > 767) {
		// console.log((document.body.getBoundingClientRect()).top, scrollPos);
		if (document.body.getBoundingClientRect().top > scrollPos) {
			header.classList.add("fixed");
			header.classList.remove("hide");
			if (this.pageYOffset <= 34) {
				header.classList.remove("fixed");
			}
		}
		else {
			header.classList.add("fixed");
			header.classList.add("hide");
		}
	}
	else {
		// console.log((document.body.getBoundingClientRect()).top, scrollPos);
		if (document.body.getBoundingClientRect().top > scrollPos) {
			header.classList.add("fixed");
			if (this.pageYOffset <= 34) {
				header.classList.remove("fixed");
			}
		}
		else {
			header.classList.add("fixed");
		}
	}

	// saves the new position for iteration.
	scrollPos = document.body.getBoundingClientRect().top;
});




/* Cookie */
function Cookie()
{

}

// Initialize Cookie object
Cookie.set = function (name, value, expiration)
{
	if (expiration)
	{
		var date = new Date(expiration);
		//date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		var expires = "; expires=" + date.toGMTString();
	}
	else var expires = "";
	document.cookie = name + "=" + value + expires + "; path=/";
}

Cookie.get = function (name)
{
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for (var i = 0; i < ca.length; i++)
	{
		var c = ca[i];
		while (c.charAt(0) == ' ') c = c.substring(1, c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
	}
	return null;
}

Cookie.remove = function (name)
{
	setCookie(name, "", -1);
}
