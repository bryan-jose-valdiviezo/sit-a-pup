"use strict";
function SetUserConnection(userId) {
    if (userId == null || userId == '' || userId == '0')
        var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub").build();
    else
        var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();


    document.getElementById("sendButton").disabled = true;


    connection.on("SendMessageToUser", function (user, message) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);

        li.textContent = `${user} says ${message}`;
    });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click", function
        (event) {
        var username = document.getElementById("senderInput").value;
        var receiverId = document.getElementById("receiverInput").value;
        var message = document.getElementById("messageInput").value;
        connection.invoke("SendNewMessage", receiverId, username, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}