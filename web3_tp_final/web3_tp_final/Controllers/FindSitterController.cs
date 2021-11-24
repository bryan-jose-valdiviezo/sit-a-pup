using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.DTO;
using web3_tp_final.Helpers;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class FindSitterController : BaseController
    {
        private static APIController _aPIController;

        public FindSitterController(APIController aPIController)
        {
            _aPIController = aPIController;
        }

        public async Task<IActionResult> Index()
        {
            if (CurrentUser() != null)
                ViewBag.CurrentUserID = CurrentUser().UserID;
            else
                ViewBag.CurrentUserID = 0;

            IEnumerable <User> users = await _aPIController.Get<User>();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByAvailability(DateTime startDate, DateTime endDate)
        {
            if (CurrentUser() != null)
                ViewBag.CurrentUserID = CurrentUser().UserID;
            else
                ViewBag.CurrentUserID = 0;
            IEnumerable<User> users = await _aPIController.Get<User>();
            List<User> availableUsers = new();
            availableUsers.Add(new Models.User { UserID = 12234565, UserName = "AvailableUser" });
            return View(availableUsers);
        }

        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> BookAppointment(int id)
        {
            if (CurrentUser() == null)
                return RedirectToAction("Index", "Home");

            User sitter = await _aPIController.Get<User>(id);
            User currentUser = await _aPIController.Get<User>(CurrentUser().UserID);
            ViewBag.sitter = sitter;
            ViewBag.user = currentUser;
            return View("Form");
        }

        [HttpPost]
        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> Create(int id, [Bind("AppointmentID, OwnerId, SitterId, StartDate, EndDate, PetIds")] AppointmentDTO appointmentForm)
        {
            if (CurrentUser() == null)
                RedirectToAction("Index", "Home");

            if((appointmentForm.OwnerId != null && appointmentForm.OwnerId == CurrentUser().UserID) && (appointmentForm.SitterId != null && appointmentForm.SitterId == id))
            {
                Appointment newAppointment = await _aPIController.PostAppointment(appointmentForm);
                if (newAppointment != null)
                {
                    return RedirectToAction("Details", "Appointments", new { userID = CurrentUser().UserID, id = newAppointment.AppointmentID });
                }
            }

            return RedirectToAction("BookAppointment", "FindSitter", new { id = id });
        }
    }
}
