$(document).ready(function () {
    $("#login-modal").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        },
        title: "Login Page",
        modal: true,
        open: function () {
            jQuery('.ui-widget-overlay').bind('click', function () {
                jQuery('#login-modal').dialog('close');
            });
        }
    });

    $("#signup-modal").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "blind",
            duration: 500
        },
        title: "Sign Up Page",
        modal: true,
        open: function () {
            jQuery('.ui-widget-overlay').bind('click', function () {
                jQuery('#signup-modal').dialog('close');
            });
        }
    });

    $('textarea').each(function () {
        this.setAttribute('style', 'height:' + (this.scrollHeight) + 'px;overflow-y:hidden;');
    }).on('input', function () {
        this.style.height = 'auto';
        this.style.height = (this.scrollHeight) + 'px';
    });
    
});



function RedirectToUrl(data) {
    window.location.href = data;
}

function SetErrorMessageLoginForm(data) {
    data = data.responseJSON;
    $.each(data, function (index, element) {
        if (element.key == "") {
            var list = "";
            $.each(element.errors, function (i, el) {
                list += '<li>' + el + '</li>';
            });
            $('#login-form div[data-valmsg-summary="true"] ul').html(list);
        } else {
            $('#login-form span[data-valmsg-for="' + element.key + '"]').html(element.errors);
        }
    });
}

function SetErrorMessageSignUpForm(data) {
    data = data.responseJSON;
    $.each(data, function (index, element) {
        if (element.key == "") {
            var list = "";
            $.each(element.errors, function (i, el) {
                list += '<li>' + el + '</li>';
            });
            $('#signup-form div[data-valmsg-summary="true"] ul').html(list);
        } else {
            $('#signup-form span[data-valmsg-for="' + element.key + '"]').html(element.errors);
        }
    });
}



function validationResult(data) {
    data = data.responseJSON;
    $.each(data, function (index, element) {
        $('span[data-valmsg-for="' + element.key + '"]').html(element.errors); //make list of errors
    });
}

$.ajaxSetup({
    global: true,
});

$(document).ajaxError(function (e, xhr) {
    if (xhr.status == 401)
        $('#login-button').trigger('click');

    if (xhr.status == 403) {
        noty({
            text: 'Permission denied',
            type: 'error',
            layout: 'topLeft',
            timeout: 3000,
            theme: 'relax'
        });
    }

    if (xhr.status == 404) {
        noty({
            text: 'Not found',
            type: 'error',
            layout: 'topLeft',
            timeout: 3000,
            theme: 'relax'
        });
    }
});

function updateComments(data) {
    var comments = "";
    $.each(data, function (index, element) {
        comments += '<li>' + element.Body + '</li>';
    });
    $('#comments').html(comments);
    $('#Body').val('');
    $('textarea').trigger('input');
}

