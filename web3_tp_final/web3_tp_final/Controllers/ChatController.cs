using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [Route("Chat/{userId}")]
        public async Task<IActionResult> Index(int userId) {
            if (CurrentUser() == null)
                return RedirectToAction("Index", "Home");
           
            User receiver = await _api.Get<User>(userId);
            User sender = await _api.Get<User>(CurrentUser().UserID);
           

            ViewBag.receiver = receiver;
            ViewBag.sender = sender;
           
            
            return View();
        }

       
    }
}
