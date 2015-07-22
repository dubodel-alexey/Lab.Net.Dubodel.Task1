function showNoty(type, message) {
    noty({
        text: message,
        type: type,
        layout: 'topLeft',
        timeout: 4000,
        theme: 'relax'
    });
}

function ShowError(err, textStatus, errorThrown) {
    if (textStatus) {
        if (textStatus === 'parsererror') {
            showNoty('error', 'Requested JSON parse failed.');
        } else if (textStatus === 'timeout') {
            showNoty('error', 'Time out error.');
        } else if (textStatus === 'abort') {
            showNoty('error', 'Ajax request aborted.');
        } else {
            showNoty('error', errorThrown);
        }
    } else {
        var msg = $.parseJSON(err).message;
        if (msg) {
            showNoty('error', msg);
        }
    }
}

function ShowUser(user) {
    $('#users').html('<li>' + user.id + '  ' + user.Name + '  ' + user.Address + '</li>');
}

function ShowUsers(users) {
    var usersList = "";
    $.each(users, function (index, element) {
        usersList += '<li>' + element.id + '  ' + element.Name + '  ' + element.Address + '</li>';
    });
    $('#users').html(usersList);
}

function ShowMessage(message) {
    $('#message').html(message);
}

$(document).ready(function () {
    $('#Get').click(function () {
        var $userId = $('#user-id');
        var serviceUrl = "http://localhost/restfulusersservice/user/";
        var successCallBack;

        if ($userId.val() == "") {
            successCallBack = ShowUsers;
        } else {
            if (isNaN(parseInt($userId.val()))) {
                $userId.val('');
                showNoty('error', 'id should be an integer');
            } else {
                serviceUrl += $userId.val();
                successCallBack = ShowUser;
            }
        }
        if (successCallBack) {
            $.ajax({
                type: "GET",
                url: serviceUrl,
                dataType: 'json',

                success: successCallBack,
                error: ShowError
            });
        }
    });

    $('#Add').click(function () {
        var serviceUrl = "http://localhost/restfulusersservice/user";
        var userData = JSON.stringify({
            id: "0",
            Name: $('#user-name').val(),
            Address: $('#user-address').val()
        });
        $.ajax({
            type: "POST",
            url: serviceUrl,
            contentType: 'application/json; charset=UTF-8',
            data: userData,
            dataType: 'json',
            success: ShowMessage,
            error: ShowError
        });
    });

    $('#Update').click(function () {
        var serviceUrl = "http://localhost/restfulusersservice/user";
        var userData = JSON.stringify({
            id: $('#user-id').val(),
            Name: $('#user-name').val(),
            Address: $('#user-address').val()
        });
        $.ajax({
            type: "PUT",
            url: serviceUrl,
            contentType: 'application/json; charset=UTF-8',
            data: userData,
            dataType: 'json',
            success: ShowMessage,
            error: ShowError
        });
    });

    $('#Delete').click(function () {
        var $userId = $('#user-id');
        var serviceUrl = "http://localhost/restfulusersservice/user/";

        if (isNaN(parseInt($userId.val()))) {
            $userId.val('');
            showNoty('error', 'id should be an integer');
        } else {
            serviceUrl += $userId.val();
            $.ajax({
                type: "DELETE",
                url: serviceUrl,
                success: ShowMessage,
                error: ShowError
            });
        }
    });
});