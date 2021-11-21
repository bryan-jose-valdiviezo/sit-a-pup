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
    public class FindSitterController : Controller
    {
        private static APIController _aPIController;
        public FindSitterController(APIController aPIController)
        {
            _aPIController = aPIController;
        }
        public async Task<IActionResult> Index()
        {
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
            if (user != null)
                ViewBag.CurrentUserID = user.UserID;
            else
                ViewBag.CurrentUserID = 0;
            IEnumerable <User> users = await _aPIController.Get<User>();
            return View(users);
        }

        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> BookAppointment(int id)
        {
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
            if (user == null)
                return RedirectToAction("Index", "Home");

            User sitter = await _aPIController.Get<User>(id);
            User currentUser = await _aPIController.Get<User>(user.UserID);
            ViewBag.sitter = sitter;
            ViewBag.user = currentUser;
            return View("Form");
        }

        [HttpPost]
        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> Create(int id, [Bind("AppointmentID, OwnerId, SitterId, StartDate, EndDate, PetIds")] AppointmentDTO appointmentForm)
        {
            Appointment newAppointment = await _aPIController.PostAppointment(appointmentForm);
            if (newAppointment != null)
            {
                return RedirectToAction("Details", "Appointments", new {userID = appointmentForm.OwnerId, id = newAppointment.AppointmentID });
            }
            else
            {
                User sitter = await _aPIController.Get<User>(id);
                User currentUser = await _aPIController.Get<User>(1);
                ViewBag.sitter = sitter;
                ViewBag.user = currentUser;
                return View("Form", appointmentForm);
            }
        }
    }
}
