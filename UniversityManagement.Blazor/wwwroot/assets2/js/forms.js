$(function () {

    "use strict";

    var contactForm = $('#formContact');
    contactForm.on('submit', function(e){
        $.ajax({
            url: 'mailer/contact.php',
            type: 'POST',
            data: new FormData( this ),
            processData: false,
            contentType: false,
            beforeSend: function(){
                contactForm.css({"opacity": "0.5", "pointer-events": "none"});
            },
            success: function(data) {
                var jsonData = JSON.parse(data);
                console.log(jsonData);
                contactForm[0].reset();
                contactForm.find('.mil-custom-file-input span').text('Attach your file');
                contactForm.css({"opacity": "1", "pointer-events": "all"});
                if ( jsonData.result == 'success' ) {
                    contactForm.find('.alert-success').delay(1000).fadeIn();
                    setTimeout(function(){
                        contactForm.find('.alert-success').delay(1000).fadeOut();
                    }, 5000);
                } else {
                    contactForm.find('.alert-error').delay(1000).fadeIn();
                    setTimeout(function(){
                        contactForm.find('.alert-error').delay(1000).fadeOut();
                    }, 5000);
                }
            }
        });
        e.preventDefault();
    });

    var feedbackForm = $('#formFeedback');
    feedbackForm.on('submit', function(e){
        $.ajax({
            url: 'mailer/feedback.php',
            type: 'POST',
            data: new FormData( this ),
            processData: false,
            contentType: false,
            beforeSend: function(){
                feedbackForm.css({"opacity": "0.5", "pointer-events": "none"});
            },
            success: function(data) {
                var jsonData = JSON.parse(data);
                console.log(jsonData);
                feedbackForm[0].reset();
                feedbackForm.find('.mil-custom-file-input span').text('Attach your file');
                feedbackForm.css({"opacity": "1", "pointer-events": "all"});
                if ( jsonData.result == 'success' ) {
                    feedbackForm.find('.alert-success').delay(1000).fadeIn();
                    setTimeout(function(){
                        feedbackForm.find('.alert-success').delay(1000).fadeOut();
                    }, 5000);
                } else {
                    feedbackForm.find('.alert-error').delay(1000).fadeIn();
                    setTimeout(function(){
                        feedbackForm.find('.alert-error').delay(1000).fadeOut();
                    }, 5000);
                }
            }
        });
        e.preventDefault();
    });

    var eventForm = $('#formEvent');
    eventForm.on('submit', function(e){
        $.ajax({
            url: 'mailer/event.php',
            type: 'POST',
            data: new FormData( this ),
            processData: false,
            contentType: false,
            beforeSend: function(){
                eventForm.css({"opacity": "0.5", "pointer-events": "none"});
            },
            success: function(data) {
                var jsonData = JSON.parse(data);
                console.log(jsonData);
                eventForm[0].reset();
                eventForm.css({"opacity": "1", "pointer-events": "all"});
                if ( jsonData.result == 'success' ) {
                    eventForm.find('.alert-success').delay(1000).fadeIn();
                    setTimeout(function(){
                        eventForm.find('.alert-success').delay(1000).fadeOut();
                    }, 5000);
                } else {
                    eventForm.find('.alert-error').delay(1000).fadeIn();
                    setTimeout(function(){
                        eventForm.find('.alert-error').delay(1000).fadeOut();
                    }, 5000);
                }
            }
        });
        e.preventDefault();
    });
});
