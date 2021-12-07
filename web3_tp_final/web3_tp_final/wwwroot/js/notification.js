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

    connection.on("SendMessageToUser", function (senderId, message, dateTime) {
        console.log("Message received from id " + senderId);

        IsInCurrentConversation(senderId, message);

        UpdateLastMessage(senderId, false, message);
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
            connection.invoke("SendNewMessage", userIdInt, recipientIdInt, content).catch(function (err) {
                return console.error(err.toString());
            });
            console.log("Ajout du message réussi.");
            GetMessageLine(userIdInt, content);

            UpdateLastMessage(recipientIdInt, true, content);
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

function UpdateLastMessage(id, isUser, message) {
    var ConversationCase = $('#ConversationRecipient_' + id);
    if (ConversationCase.length == 0) {
        $.ajax({
            url: '/Chat/ReloadConversationList',
            type: 'GET',
            dataType: 'html',
            success: function (data) {
                $('#ConversationList').html(data);
            },
            error: function (xhr) {
                console.log("Error in reloading conversations list");
            }
        });
    }
    else if (isUser) {
        ConversationCase.text("You: " + message);
    }
    else {
        ConversationCase.text(message);
    }
}

function IsInCurrentConversation(senderId, message) {
    console.log("Checking if id is in current conversation: " + senderId);
    $.ajax({
        url: '/Chat/CurrentConversation',
        type: 'get',
        dataType: 'json',
        data: {
            recipientID: senderId
        },
        success: function (data) {
            console.log("Got CurrentConversation condition:" + data);
            if (data) {
                console.log("Appended message");
                GetMessageLine(0, message);
            }
            else {
                console.log("Did not add message");
            }
        },
        error: function (xhr) {
            console.log("Error in gettting CurrentConversation condition");
            return false;
        }
    });
}

function SwitchConversation(conversationBox, id) {
    $('.last-message').removeClass('active-conversation');
    conversationBox.toggleClass('active-conversation');

    var recipientId = id;
    console.log("Recipient id to send : " + recipientId);
    $.ajax({
        url: '/Chat/ShowConversation',
        dataType: 'html',
        type: 'GET',
        data: {
            recipientID: recipientId
        },
        success: function (data) {
            console.log("Success in fetching conversation, applying to screen");
            $('#MainConversation').html(data);
        },
        error: function (xhr) {
            console.log("Error in switching conversation");
        }
    });
}