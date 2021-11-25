﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Controllers;
using web3_tp_final.Helpers;
using web3_tp_final.Models;

namespace web3_tp_final
{
    public class SitterController : BaseController
    {
        private static APIController _aPIController;

        public SitterController(APIController aPIController)
        {
            _aPIController = aPIController;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Availabilities = await _aPIController.GetAvailabilitiesForUser(CurrentUser().UserID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityID,StartDate,EndDate,UserId")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                availability.UserId = CurrentUser().UserID;
                await _aPIController.Post<Availability>(availability);
                ViewBag.Availabilities = await _aPIController.GetAvailabilitiesForUser(CurrentUser().UserID);
            }
            return View("Index");
        }
    }
}
