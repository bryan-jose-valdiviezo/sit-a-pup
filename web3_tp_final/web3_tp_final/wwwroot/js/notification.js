"use strict";
var connection;


function SetUserConnection(userId) {
    if (userId == null || userId == '' || userId == '0')
        connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub").build();
    else
        connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();

    connection.on("sendNewFormToUser", (appointmentId) => {
        $.ajax({
            url: '/Home/GetNotification',
            type: 'get',
            dataType: 'html',
            data: {
                appointmentId: appointmentId
            },
            success: function (data) {
                $('#notification-section').append(data);
                $('#Notification_Appointment_' + appointmentId).toast('show');
                $('#Notification_Appointment_' + appointmentId).on('hidden.bs.toast', function () {
                    $(this).remove();
                });
            },
            error: function (xhr) {
                alert("Error ajax");
                alert(xhr.responseJSON);
            }
        });
    });

    connection.on("sendToAll", (message) => {
        alert(message);
    });

    connection.on("SendMessageToUser", function (message, dateTime) {
        GetMessageLine(0, message);
    });

    connection.start().catch(function (err) {
        return alert(err.toString());
    });
}

function SendConversationMessage() {
    var url = "https://localhost:44308/api/Messages"
    var userIdInt = $('#SenderID').val();
    var recipientIdInt = $('#RecipientID').val();
    var content = $('#NextMessage').val();

    var date = new Date($.now());
    var dateTime = date.getDate() + "-" + (date.getMonth() + 1) + "-" + date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    console.log(dateTime);
    var variable = {

        'content': content,
        'timeStamp': dateTime,
        'sender': userIdInt,
        'recipient': recipientIdInt,
        'userId': null,
        'user': null

    }

    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        datatype: 'json',
        data: JSON.stringify(variable),


        success: function (data) {
            $('#NextMessage').val("");
            connection.invoke("SendNewMessage", recipientIdInt, content).catch(function (err) {
                return console.error(err.toString());
            });
            console.log("Ajout du message réussi.");
            GetMessageLine(userIdInt, content);
        },
        error: function (data) {
            alert("Error: " + data.responseText);
        }
    });
}

function GetMessageLine(id, message) {
    $.ajax({
        url: '/Chat/GetMessageLine',
        type: 'GET',
        dataType: 'html',
        data: {
            id: id,
            message: message
        },
        success: function (data) {
            console.log("Success in getting line from GetMEssageLine");
            $('#CurrentConversation').append(data);
        },
        error: function (xhr) {
            console.log("Error in GetMessageLine");
        }
    });
}