using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
            if (CheckIfUserIsConnected())
            {
                ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
                return View();
            }
            
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityID,StartDate,EndDate,UserId")] AvailabilityDTO availability)
        {
            if (CheckIfUserIsConnected())
            {
                if (ModelState.IsValid)
                {
                    availability.UserId = GetCurrentUser().UserID;
                    await _api.PostAvailability(availability);
                    ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
                }
                return View("Index");
            }

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (CheckIfUserIsConnected())
            {
                await _api.Delete<Availability>(id);
                ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
                return View("Index");
            }

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> ConfirmDeletion(int id)
        {
            if (CheckIfUserIsConnected())
            {
                await _api.Delete<Availability>(id);
                ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
                return View("Index");
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
