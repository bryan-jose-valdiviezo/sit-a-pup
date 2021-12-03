using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Controllers;
using web3_tp_final.DTO;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;

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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            //await _api.Delete<Availability>(id);
            ViewBag.Availabilities = await _api.GetAvailabilitiesForUser(GetCurrentUser().UserID);
            return View("Index");
        }
    }
}
