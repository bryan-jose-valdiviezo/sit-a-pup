using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class MessagesController : BaseController
    {
        private IEnumerable<Message> _messages;
        public MessagesController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) :
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        public async Task<List<Message>> Index()
        {
            _messages = await _api.Get<Message>();

            return (List<Message>)_messages;
        }

       /* [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<Message> Create([Bind("MessageID,Content, TimeStamp,Sender,Recipient")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.MessageID = 0;
                Message createdMessage = await _api.Post<Message>(message);
                if (createdMessage != null)
                    return message;
            }

            return null;
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Email,Address,PhoneNumber")] Message message)
        {
            if (id != message.MessageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    message.MessageID = id;
                    Message updatedMessage = await _api.Put<Message>(id, message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MessageID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }



        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _api.Delete<Message>(id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        private bool MessageExists(int id)
        {
            return _messages.Any(e => e.MessageID == id);
        }*/
    }
}
