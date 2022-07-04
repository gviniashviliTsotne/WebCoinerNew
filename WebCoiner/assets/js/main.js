$(document).ready(function() {

	// FIXES and BLACK MAGIC
	
	$.fn.andSelf = function() { return this.addBack.apply(this, arguments); }

	AOS.init({disable: 'mobile' });

	// POPUPS

	function closeAllPopups(){
		$('.popup-section').removeClass('opened');
	}

	$('.menu-open').on('click touchstart', function(e){
		e.stopPropagation();
		e.preventDefault();
		closeAllPopups(e);
		$('#mob-menu').addClass('opened');
	});

	$('.action-button').on('click touchstart', function(e){
		e.stopPropagation();
		e.preventDefault();
		var form_from = $(this).attr('id');
		$('#form [name="requset_from"]').val(form_from);
		$('#form').addClass('opened');
	});


	$('.popup-close').on('click touchstart', function(e){
		e.stopPropagation();
		e.preventDefault();
		closeAllPopups()
	});

	// PARTICLES

	particlesJS("top-section", {
		"particles": {
			"number": {
				"value": 80,
				"density": {
					"enable": true,
					"value_area": 800
				}
			},
			"color": {
				"value": "#adadad"
			},
			"shape": {
				"type": "circle",
				"stroke": {
					"width": 0,
					"color": "#000000"
				},
				"polygon": {
					"nb_sides": 5
				},
				"image": {
					"src": "img/github.svg",
					"width": 100,
					"height": 100
				}
			},
			"opacity": {
				"value": 0.49716301422833176,
				"random": false,
				"anim": {
					"enable": false,
					"speed": 1,
					"opacity_min": 0.1,
					"sync": false
				}
			},
			"size": {
				"value": 3,
				"random": true,
				"anim": {
					"enable": false,
					"speed": 40,
					"size_min": 0.1,
					"sync": false
				}
			},
			"line_linked": {
				"enable": true,
				"distance": 150,
				"color": "#adadad",
				"opacity": 0.4,
				"width": 1
			},
			"move": {
				"enable": true,
				"speed": 2,
				"direction": "none",
				"random": false,
				"straight": false,
				"out_mode": "out",
				"bounce": false,
				"attract": {
					"enable": false,
					"rotateX": 600,
					"rotateY": 1200
				}
			}
		},
		"interactivity": {
			"detect_on": "canvas",
			"events": {
				"onhover": {
					"enable": false,
					"mode": "repulse"
				},
				"onclick": {
					"enable": false,
					"mode": "push"
				},
				"resize": true
			},
			"modes": {
				"grab": {
					"distance": 400,
					"line_linked": {
						"opacity": 1
					}
				},
				"bubble": {
					"distance": 400,
					"size": 40,
					"duration": 2,
					"opacity": 8,
					"speed": 3
				},
				"repulse": {
					"distance": 200,
					"duration": 0.4
				},
				"push": {
					"particles_nb": 4
				},
				"remove": {
					"particles_nb": 2
				}
			}
		},
		"retina_detect": true
	});

	// NAVIGATION

	var scrollScreen = function(e){
		e.preventDefault();
		closeAllPopups();
		var id = $(this).attr('href');
		if (navigator.userAgent.match(/(iPod|iPhone|iPad)/)) {
			$(id)[0].scrollIntoView({ block: 'start',  behavior: 'smooth' });
		}
		else{
			$('html, body').animate({
				scrollTop: ($(id).offset().top)
			}, 1000);
		}
	}

	$(".navigation a").on('click touchstart', scrollScreen);
	$("#learn-more").on('click touchstart', scrollScreen);

	// CAROUSEL
	$("body").on("click touchend", ".asis-tobe-carousel-control", function(e) {
		e.preventDefault();
		$('.asis-tobe-carousel-slide').toggleClass('active');
	});

	// MESSENGER PICKER
	
	$('.feedback-form .messenger').on('click touchstart', function (e) {
		e.stopPropagation();
		e.preventDefault();
		var form = $(this).parents('form');
		form.find('.messenger').removeClass('active');
		$(this).addClass('active');
		var messenger = $(this).data('messenger');

		form.find('[for="user_id"]').html(messenger);
	});

	// INPUTS

	$('.input-box input').focusout(function(e) {
		if ($(this).val().length > 0) {
			$(this).parent(".input-box").find(".collapsed").addClass("active");
		}
		else{
			$(this).parent(".input-box").find(".collapsed").removeClass("active");
		}
	});

	// FORM

	$(document).on('submit','#feedback' ,function(event){
		var validator = new Validator();
		var current_form = event.target;
		var validationResult = validator.validateForm(current_form);
		if (validationResult){
			event.preventDefault();
			var action = $(this).attr("action");
			var data = {
				messenger:			$("[name=messenger]").val(),
				user_id:			$("[name=user_id]").val(),
				question:			$("[name=question]").val(),
				additional_field:	$("[name=additional_field]").val(),
			};
			$.ajax({
				type: "POST",
				dataType: "json",
				url: action,
				data: data,
				success: function(responce) {
					if (responce) {
						console.log(responce);
					}
					
				},
				error: function(jqXHR, textStatus, errorThrown) {
					console.log("Error: " + errorThrown);
				}
			});
			$('#thx').addClass('opened');
		}
		return false;
	});

});