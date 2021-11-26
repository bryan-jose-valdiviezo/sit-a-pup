using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Controllers;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final
{
    public class SitterController : BaseController
    {
        public SitterController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) :
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(CurrentUser().UserID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityID,StartDate,EndDate,UserId")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                availability.UserId = CurrentUser().UserID;
                await _api.Post<Availability>(availability);
                ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(CurrentUser().UserID);
            }
            return View("Index");
        }
    }
}
