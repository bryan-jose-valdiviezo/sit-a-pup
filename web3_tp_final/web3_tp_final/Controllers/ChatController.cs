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

        [Route("Chat/GetMessageLine")]
        public async Task<IActionResult> GetMessageLine(int id, string message)
        {
            ViewBag.Message = message;
            ViewBag.IsUser = (id == GetCurrentUser().UserID);
            return PartialView("_MessageLine");
        }

        [Route("Chat/Conversation/{id}")]
        public async Task<IActionResult> Conversation(int? id)
        {
            int RecipientID;
            if (id != null)
            {
                RecipientID = (int) id;
            }
            else
            {
                RecipientID = 2;
            }

            ConversationDTO conversation = await _api.GetConversation(GetCurrentUser().UserID, RecipientID);
            return View(conversation);
        }

        [Route("Chat/Index/{userId}")]
        public async Task<IActionResult> Index(int userId) {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Home");
           
            User receiver = await _api.Get<User>(userId);
            User sender = await _api.Get<User>(GetCurrentUser().UserID);
            List<Message> messages= await _api.GetConversationBetweenTwoUsers(userId);

            ViewBag.receiver = receiver;
            ViewBag.sender = sender;
           
            
            return View(messages);
        }

       
    }
}
