"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
 
    connection.on("ReceiveMessage", function (sender, message) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);

        li.textContent = `${sender} says ${message}`;
    });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

document.getElementById("sendButton").addEventListener("click", function
    (event) {

    var sender = document.getElementById("senderInput").value;
    var user = document.getElementById("receiverInput").value;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendPrivateMessage", user, message).catch(function (err) {
        return console.error(err.toString());
        /* if (receiver != "") {
 
             connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err) {
                 return console.error(err.toString());
             });
         }
         else {
             connection.invoke("SendMessage", sender, message).catch(function (err) {
                 return console.error(err.toString());
             });
         }*/


        event.preventDefault();
    });
});
