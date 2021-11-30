"use strict";
var username="";
var receiverId="";
var message="";
var userChat ="";

function setUsername(user) {
    username = user;
}

function setReceiverId(receiver) {
    receiverId= receiver;
}

function setCurrentUserId(current) {
    currentUserId = current;
}

function SetUserConnection(userId) {
    if (userId == null || userId == '' || userId == '0')
        var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub").build();
    else
        var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();


    document.getElementById("sendButton").disabled = true;


   

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click", function
        (event) {
        username = document.getElementById("senderInput").value;
        setUsername(username);
        receiverId = document.getElementById("receiverInput").value;
        setReceiverId(receiverId);
        message = document.getElementById("messageInput").value;
        
        
        connection.invoke("SendNewMessage", receiverId, username, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

    connection.on("SendMessageToUser", function (user, message) {
        var currentUserName = document.getElementById("userChat").value;
        
        if (user.localeCompare(currentUserName) == 0) {
            var li = document.createElement("li");
            document.getElementById("messagesList").appendChild(li);

            li.textContent = `${user} says ${message}`;
        }
    });
}