"use strict";
var username="";
var receiverId = "";
var senderId = "";
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
        senderId = document.getElementById("senderId").value;
        setUsername(username);
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
   


    connection.on("SendMessageToUser", function (user, message,dateTime) {
        var currentUserName = document.getElementById("userChat").value;
        document.getElementById("timeStamp").value = dateTime;
        document.getElementById("content").value = message;
        if (user.localeCompare(currentUserName) == 0) {
            var li = document.createElement("li");
            li.className = "container-chat sender";
            li.style = "list-style-type: none"
            document.getElementById("messagesList").appendChild(li);
           
            li.textContent = `${user} says: ${message}`;


            
            /* let container = document.createElement('div');
             container.className = "container sender";
             let sender = document.createElement('p');
             sender.className = "sender";
             sender.innerHTML = user;
             let text = document.createElement('p');
             text.innerHTML = message;

             container.appendChild(sender);
             container.appendChild(text);
             document.getElementById("messagesList").appendChild(container);
             */
             userId = document.getElementById("senderId").value;
             recipientId = document.getElementById("recipientInput").value;

            postMessage(userId, message, recipientId, dateTime);

             
        }
    });


    function postMessage(userId,message,recipientId,dateTime) {
        var url = "https://localhost:44308/api/Messages"
        var userIdInt = parseInt(userId);
        var recipientIdInt = parseInt(recipientId);
        console.log(dateTime);
        var variable = {
            
            'content': message,
            'timeStamp':dateTime,
            'sender': userIdInt,
            'recipient': recipientIdInt,
            'userId': null,
            'user':null
            
        }

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            datatype: 'json',
            data: JSON.stringify(variable),


            success: function (data) {
                console.log("Ajout du message réussi.");

            },
            error: function (data) {
alert("Error: " + data.responseText);
}
        });
    }
}