using Microsoft.AspNetCore.Mvc;
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
    public class SitterController : Controller
    {
        private static APIController _aPIController;
        private static MockAvailability _mockAvailability = new();

        public SitterController(APIController aPIController)
        {
            _aPIController = aPIController;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityID,StartDate,EndDate,UserId")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
                availability.UserId = user.UserID;
                _mockAvailability.Availabilities.Add(availability);
                ViewBag.Availabilities = _mockAvailability.Availabilities;
            }
            return View("Index");
        }
    }
}
