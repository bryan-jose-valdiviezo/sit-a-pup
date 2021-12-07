using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
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
            if (GetCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }

            CleanupExpiredAvailabilities(await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID));
            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityID,StartDate,EndDate,UserId")] AvailabilityDTO availability)
        {
            if (GetCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Availability> availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            ViewBag.Availabilities = availabilities;
            
            if (ModelState.IsValid)
            {
                if (CheckIfAvailabalityIsOverlapping(availabilities, availability))
                {
                    ModelState.AddModelError(nameof(AvailabilityDTO.StartDate), "Vous êtes déjà disponible à ce moment");
                    return View("Index", availability);
                }

                availability.UserId = GetCurrentUser().UserID;
                await _api.PostAvailability(availability);
                ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            }

            return View("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (GetCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Availability availabilityToDelete = await _api.Get<Availability>(id);

            if (GetCurrentUser().UserID == availabilityToDelete.UserId)
            {
                await _api.Delete<Availability>(id);
            }

            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);

            return View("Index");
        }

        public async Task<IActionResult> ConfirmDeletion(int id)
        {
            if (GetCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }

            await _api.Delete<Availability>(id);
            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            return View("Index");
        }

        private bool CheckIfAvailabalityIsOverlapping(List<Availability> availabilities, AvailabilityDTO availabilityDTO)
        {
            foreach (Availability availability in availabilities)
            {
                if (availabilityDTO.StartDate.Date <= availability.EndDate.Date)
                {
                    return true;
                }
            }
            return false;
        }

        private async void CleanupExpiredAvailabilities(List<Availability> availabilities)
        {
            foreach (Availability availability in availabilities)
            {
                if (availability.EndDate.Date < DateTime.Now.Date)
                {
                    await _api.Delete<Availability>(availability.AvailabilityID);
                }
            }
        }
    }
}
