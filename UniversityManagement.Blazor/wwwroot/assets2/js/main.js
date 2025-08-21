/* -------------------------------------------

Name: 		ITSulu
Version:    1.1
Developer:	Nazar Miller (millerDigitalDesign)
Portfolio:  https://themeforest.net/user/millerdigitaldesign/portfolio?ref=MillerDigitalDesign

p.s. I am available for Freelance hire (UI design, web development). email: miller.themes@gmail.com

------------------------------------------- */

$(function () {

    "use strict";

    var acc = document.getElementsByClassName("mil-accordion");
    var i;

    for (i = 0; i < acc.length; i++) {
        acc[i].onclick = function () {
            this.classList.toggle("mil-active");
            var panel = this.nextElementSibling;
            if (panel.style.maxHeight) {
                panel.style.maxHeight = null;
            } else {
                panel.style.maxHeight = panel.scrollHeight + "px";
            }
        }
    }

    $(".mil-menu-btn").on("click", function () {
        $(this).toggleClass('mil-active');
        $('.mil-navigation').toggleClass('mil-active');
    });

    $(window).on("scroll", function () {
        var scroll = $(window).scrollTop();

        if (scroll >= 220) {
            $(".mil-top-panel.mil-animated").removeClass("mil-top-panel-transparent");
            $(".has-additional-panel").addClass("mil-hide-top");
        } else {
            $(".mil-top-panel.mil-animated").addClass("mil-top-panel-transparent");
            $(".has-additional-panel").removeClass("mil-hide-top");
        }
    });

    var swiper = new Swiper('.mil-banner-slideshow', {
        slidesPerView: 1,
        spaceBetween: 0,
        speed: 1500,
        autoplay: true,
        effect: 'fade',
        parallax: true,
        autoplay: {
            delay: 5000,
        },
        loop: true,
        pagination: {
            el: '.mil-pagination',
            type: 'bullets',
            clickable: true,
        },
    });

    var swiper = new Swiper('.mil-banner-slider', {
        slidesPerView: 1,
        spaceBetween: 0,
        speed: 1500,
        autoplay: true,
        effect: 'fade',
        parallax: true,
        autoplay: {
            delay: 5000,
        },
        loop: true,
        navigation: {
            prevEl: '.mil-banner-prev',
            nextEl: '.mil-banner-next',
        },
    });

    var swiper = new Swiper('.mil-works-slider', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        navigation: {
            prevEl: '.mil-works-prev',
            nextEl: '.mil-works-next',
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
            }
        },
    });
    var swiper = new Swiper('.mil-blog-slider', {
        spaceBetween: 30,
        speed: 800,
        slidesPerView: 1,
        navigation: {
            prevEl: '.mil-blog-prev',
            nextEl: '.mil-blog-next',
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 2,
            },
            1200: {
                slidesPerView: "auto",
            }
        },
    });
    var swiper = new Swiper('.mil-revi-slider', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        navigation: {
            prevEl: '.mil-revi-prev',
            nextEl: '.mil-revi-next',
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 3,
            }
        },
    });

    var swiper = new Swiper('.mil-revi-slider-2', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        autoplay: {
            delay: 5000,
        },
        loop: true,
        navigation: {
            prevEl: '.mil-revi-prev',
            nextEl: '.mil-revi-next',
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
            },
        },
    });

    var swiper = new Swiper('.mil-services-slider', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        navigation: {
            prevEl: '.mil-services-prev',
            nextEl: '.mil-services-next',
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 3,
            }
        },
    });

    var swiper = new Swiper('.mil-events-slider', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        navigation: {
            prevEl: '.mil-events-prev',
            nextEl: '.mil-events-next',
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 3,
            }
        },
    });

    var swiper = new Swiper('.mil-courses-slider', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        navigation: {
            prevEl: '.mil-courses-prev',
            nextEl: '.mil-courses-next',
        },
        breakpoints: {
            768: {
                slidesPerView: 2,
            },
            992: {
                slidesPerView: 3,
            }
        },
    });

    var swiper = new Swiper('.mil-banners-slider', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        autoplay: {
            delay: 5000,
        },
        effect: 'fade',
        parallax: true,
        loop: true,
        pagination: {
            el: '.mil-banners-pagination',
            type: 'bullets',
            clickable: true,
        },
    });

    var swiper = new Swiper('.mil-box-slider', {
        slidesPerView: 1,
        spaceBetween: 30,
        speed: 800,
        parallax: true,
        effect: 'fade',
        navigation: {
            prevEl: '.mil-box-prev',
            nextEl: '.mil-box-next',
        },
    });

    var swiper = new Swiper('.mil-tabs-slider', {
        slidesPerView: 1,
        speed: 800,
        allowTouchMove: false,
        hashNavigation: {
            watchState: true,
        },
        effect: 'fade',
        parallax: true,
    });

    $(".mil-tab-buttons a").on("click", function () {
        $('.mil-tab-buttons a').removeClass('mil-active');
        $(this).toggleClass('mil-active');
    });

    var swiper = new Swiper('.mil-tabs-slider-2', {
        slidesPerView: 1,
        speed: 800,
        allowTouchMove: false,
        hashNavigation: {
            watchState: true,
        },
        effect: 'fade',
        parallax: true,
    });

    $(".mil-tabs-left-nav a").on("click", function () {
        $('.mil-tabs-left-nav a').removeClass('mil-active');
        $(this).toggleClass('mil-active');
    });

    const input = document.getElementById('mil-file-input');
    const label = document.getElementsByClassName("mil-custom-file-input")[0];
    
    if ( input != undefined ) {
        input.addEventListener("change", () => {
            const file = input.value;
            const [fileName] = file.match(/\w+.\w+$/);

            if (fileName.trim()) {
                label.classList.add("mil-with-file");
                label.querySelector('span').innerText = fileName;
            }
        });
    }
});
