using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.DTO;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class ChatController : BaseController
    {
        public ChatController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) :
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        [Route("Chat/ReloadConversationList")]
        public async Task<IActionResult> ReloadConversationList()
        {
            List<KeyValuePair<string, Message>> lastMessages =  await _api.GetLastMessages(GetCurrentUser().UserID);
            ViewBag.CurrentRecipient = GetCurrentRecipient();
            return PartialView("_ConversationList", lastMessages);
        }

        [Route("Chat/CurrentConversation")]
        public async Task<IActionResult> CurrentConversation(int recipientID)
        {
            Debug.WriteLine("Checking if id is in conversation: " + recipientID);
            Debug.WriteLine("Current recipient id: " + GetCurrentRecipient().UserID);
            if (GetCurrentRecipient() != null && GetCurrentRecipient().UserID == recipientID)
                return Json(true);
            else
                return Json(false);
        }

        [Route("Chat/ShowConversation")]
        public async Task<IActionResult> ShowConversation(int recipientID)
        {
            User recipient;
            Debug.WriteLine("Recipient ID received: " + recipientID);
            recipient = await SetCurrentRecipient(recipientID);
            ViewBag.CurrentRecipient = recipient;
            ConversationDTO conversation = await _api.GetConversation(GetCurrentUser().UserID, recipient.UserID);

            return PartialView("_MainConversation", conversation);
        }

        [Route("Chat/GetMessageLine")]
        public async Task<IActionResult> GetMessageLine(int id, string message)
        {
            ViewBag.Message = message;
            ViewBag.IsUser = (id == GetCurrentUser().UserID);
            return PartialView("_MessageLine");
        }

        [Route("Chat/Conversation/{id?}")]
        public async Task<IActionResult> Conversation(int? id)
        {
            List<KeyValuePair<string,Message>> lastMessages = await _api.GetLastMessages(GetCurrentUser().UserID);
            if (id == null && (lastMessages == null || lastMessages.Count == 0))
                return View("EmptyChat");

            User recipient;
            if (id != null)
            {
                recipient = await SetCurrentRecipient((int)id);
            }
            else
            {
                var firstConversation = lastMessages.First();
                if (GetCurrentRecipient() != null)
                    recipient = GetCurrentRecipient();
                else if (firstConversation.Value.Sender == GetCurrentUser().UserID)
                    recipient = await SetCurrentRecipient(firstConversation.Value.Recipient);
                else
                    recipient = await SetCurrentRecipient(firstConversation.Value.Sender);
            }

            ViewBag.LastMessages = await _api.GetLastMessages(GetCurrentUser().UserID);
            ViewBag.CurrentRecipient = recipient;
            ConversationDTO conversation = await _api.GetConversation(GetCurrentUser().UserID, recipient.UserID);
            return View(conversation);
        }

        [Route("Chat/Index/{userId}")]
        public async Task<IActionResult> Index(int userId) {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Login");
           
            User receiver = await _api.Get<User>(userId);
            User sender = await _api.Get<User>(GetCurrentUser().UserID);
            List<Message> messages= await _api.GetConversationBetweenTwoUsers(userId);

            ViewBag.receiver = receiver;
            ViewBag.sender = sender;
           
            return View(messages);
        }
    }
}
