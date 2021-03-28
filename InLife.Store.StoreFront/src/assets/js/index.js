var position_top = "0";
$(document).ready(function () {
	$('.hero-slider a[href*="#"]:not([href="#"])').click(function () {
		if (location.pathname.replace(/^\//, "") == this.pathname.replace(/^\//, "")
			&& location.hostname == this.hostname) {
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



		//$('.btnLearn').click(function () {
		$(document).on('click', '.btnLearn', function (event) {
			var link = $(this).attr("data-buy-link");
			$("#confirmation2").prop("href", link);
		});

		$(document).on('click', '.btnBuy', function (event) {
			var link = $(this).attr("data-buy-link");
			$("#confirmation").prop("href", link);
		});
		/*var windowSize = $(window).width();
	
		if(windowSize <= 2560 && windowSize >= 1921) {
			$('.hero-banner').attr('src', 'assets/images/2560px.jpg');
			$('.section-hero').css('background-image', 'url(assets/images/2560px.jpg)');
		}
		else if (windowSize <= 1920 && windowSize >= 1441) {
			$('.hero-banner').attr('src', 'assets/images/1920px.jpg');
			$('.section-hero').css('background-image', 'url(assets/images/1920px.jpg)');
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
			$('.hero-banner').attr('src', 'assets/images/1023px.jpg');
			$('.section-hero').css('background-image', 'none !important');
		}*/



		$(".plan-slider-sp").slick({
			centerMode: true,
			centerPadding: '60px',
			slidesToShow: 1,
			responsive: [
				{
					breakpoint: 768,
					settings: {
						arrows: false,
						centerMode: true,
						centerPadding: '40px',
						slidesToShow: 1
					}
				},
				{
					breakpoint: 480,
					settings: {
						arrows: false,
						centerMode: true,
						centerPadding: '40px',
						slidesToShow: 1
					}
				}
			]
		});

		var windowWidth = $(window).innerWidth();
		 //scroll animation

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
		

		


		$(".modal").on($.modal.OPEN, function (event, modal) {
			if (windowWidth <= 767) {
				const buttonHeight = $(this)
					.find(".modal-footer")
					.outerHeight();
				$(this)
					.find(".modal-body")
					.css("padding-bottom", buttonHeight + "px");
			}
		});




		$(".preloader .preloader-content").addClass("content-wrapper");
	});


	


	window.onload = function () {
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
	window.addEventListener("scroll", function () {

		if (mobile > 767) {
			// console.log((document.body.getBoundingClientRect()).top, scrollPos);
			if (document.body.getBoundingClientRect().top > scrollPos) {
				//header.classList.add("fixed");
				//header.classList.remove("hide");
				if (this.pageYOffset <= 34) {
					//header.classList.remove("fixed");
				}
			}
			else {
				//header.classList.add("fixed");
				//header.classList.add("hide");
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
	function Cookie() {

	}

	// Initialize Cookie object
	Cookie.set = function (name, value, expiration) {
		if (expiration) {
			var date = new Date(expiration);
			//date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
			var expires = "; expires=" + date.toGMTString();
		}
		else var expires = "";
		document.cookie = name + "=" + value + expires + "; path=/";
	}

	Cookie.get = function (name) {
		var nameEQ = name + "=";
		var ca = document.cookie.split(';');
		for (var i = 0; i < ca.length; i++) {
			var c = ca[i];
			while (c.charAt(0) == ' ') c = c.substring(1, c.length);
			if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
		}
		return null;
	}

	Cookie.remove = function (name) {
		setCookie(name, "", -1);
	}

	$(document).ready(function () {
		setTimeout(function () {
			$(".product-list li")
				.slice(0, 4)
				.show();
		}, 6000);


		// Cookie consent
		// memo

		setTimeout(function () {
			var memo = $(".memo");
			var cookieConsentKey = "has-cookie-consent";

			var hasCookieConsent = (Cookie ? Cookie.get(cookieConsentKey) : false);

			console.log(hasCookieConsent, 'Cookie');

			if (!hasCookieConsent) {
				memo.show();
			}

			$(".memo-close").on("click", e => {
				e.preventDefault();
		
				memo.hide();
		
				var expiration = new Date();
				expiration = expiration.setDate(expiration.getDate() + 90);
		
				Cookie.set(cookieConsentKey, true, expiration);
			});
		}, 3000);
		
		
	});

	
  
	$(window).scroll(function () {
		position_top = $('.section-easy-step').offset();
		if (position_top != undefined) {
			if ($(window).scrollTop() > position_top.top - 50) {
				$('.line-svg').show();
			}
			else {
				$('.line-svg').hide();
			}
		}
	});


	$(document).on('click', '.tablinks', function () {
		var allTabs = document.getElementsByClassName("tablinks");
		for (var i = 0; i < allTabs.length; i++) {
			allTabs[i].classList.remove("active");
		}
		this.classList.add("active");
		$(".faqMainDiv").hide("fast");
		$("#" + this.classList[1]).show();
	});

	$(document).on('click', '.faqMainDiv a.collapsed', function () {
		var obj = jQuery(this).parent().find('.show');
		if ($(obj).hasClass('show')) {
			$(obj).removeClass('show');
			$(obj).parent().find('a').addClass('collapsed');
			return false;
		}
		var id = $(this).closest(".tabcontent")[0].id;
		obj = $('#' + id).find(".show");
		$(obj).parent().find('a').addClass('collapsed');
		$(obj).removeClass('show');
	});

	//$("#loadmore").on("click", function (e) {
	$(document).on('click', '#loadmore', function (e) {
		e.preventDefault();
		const offset =
			$(".product-list").offset().top + $(".product-list").outerHeight();
		$(".product-list li:hidden")
			.slice(0, 4)
			.slideDown();

		if ($(".product-list li:hidden").length == 0) {
			$("#loadmore").fadeOut("slow");
		}

		console.log($('.product-list li:hidden').length);
		$("html,body").animate(
			{
				scrollTop: offset
			},
			1500
		);
	});


	$(document).ready(function () {
		$(document).on('click', '.children-menu', function (e) {
			$(this).toggleClass("toggle-item");
		});
		$(document).on({
			mouseenter: function () {
				$(".dropdown-bg").slideDown("fast").show();
				$(".sub-menu").show("fast");
			},
			mouseleave: function () {
				$(".dropdown-bg").slideUp("fast");
				$(".sub-menu").hide("fast");
			}
		}, '.children-menu');
	});
});
