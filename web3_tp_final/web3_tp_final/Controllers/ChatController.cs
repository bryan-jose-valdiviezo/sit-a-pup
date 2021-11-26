using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
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


        public IActionResult Index()
        {
            return View();
        }

        [Route("Chat/{id}")]
        public async Task<IActionResult> CommencerChat(int id) {
            if (CurrentUser() == null)
                return RedirectToAction("Index", "Home");
           
            User receiver = await _api.Get<User>(id);
            User sender = await _api.Get<User>(CurrentUser().UserID);
            var receiverIdentifier = _userConnectionManager.GetUserIdentifiers(id.ToString());
            var senderIdentifier = _userConnectionManager.GetUserIdentifiers(CurrentUser().UserID.ToString());

            ViewBag.receiver = receiver;
            ViewBag.sender = sender;
            ViewBag.receiverCon = receiverIdentifier;
            ViewBag.senderCon = senderIdentifier;
            
            return View("Index");
        }
    }
}
