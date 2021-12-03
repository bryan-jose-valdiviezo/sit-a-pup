﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Controllers;
using web3_tp_final.DTO;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final
{
    public class AvailabilityController : BaseController
    {
        public AvailabilityController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) :
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityID,StartDate,EndDate,UserId")] AvailabilityDTO availability)
        {
            if (ModelState.IsValid)
            {
                availability.UserId = GetCurrentUser().UserID;
                await _api.PostAvailability(availability);
                ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            }
            return View("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            await _api.Delete<Availability>(id);
            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            return View("Index");
        }

        public async Task<IActionResult> ConfirmDeletion(int id)
        {
            await _api.Delete<Availability>(id);
            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            return View("Index");
        }
    }
}
