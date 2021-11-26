"use strict";

function SetUserConnection(userId) {
    if (userId == null || userId == '' || userId == '0')
        var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub").build();
    else
        var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationUserHub?userId=" + userId).build();

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

    connection.start().catch(function (err) {
        return alert(err.toString());
    });
}