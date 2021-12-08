function GetSpinner() {
    return "<div class='spinner-border spinner-border-sm' role='status'>" +
            "<span class='sr-only'> Loading...</span>" +
            "</div>"
}

function ScrollChatBottom() {
    var CurrentConversation = $('#CurrentConversation');
    CurrentConversation.scrollTop(CurrentConversation.prop("scrollHeight"));
}

$(document).ready(function () {
    $(document).on('keyup', '#NextMessage', function (e) {
        var message = $('#NextMessage').val();

        if (message == "")
            $('#SendMessageToRecipient').prop('disabled', true);
        else {
            if (e.keyCode == 13)
                SendConversationMessage();
            else
                $('#SendMessageToRecipient').prop('disabled', false);
        }
    });
});