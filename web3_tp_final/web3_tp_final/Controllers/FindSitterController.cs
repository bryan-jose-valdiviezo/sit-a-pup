using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.DTO;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class FindSitterController : Controller
    {
        private APIController api;
        public FindSitterController()
        {
            api = new APIController();
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await api.Get<User>();
            return View(users);
        }

        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> BookAppointment(int id)
        {
            User sitter = await api.Get<User>(id);
            User currentUser = await api.Get<User>(1);
            ViewBag.sitter = sitter;
            ViewBag.user = currentUser;
            return View("Form");
        }

        [HttpPost]
        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> Create(int id, [Bind("AppointmentID, OwnerId, SitterId, StartDate, EndDate, PetIds")] AppointmentDTO appointmentForm)
        {
            Appointment newAppointment = await api.PostAppointment(appointmentForm);
            if (newAppointment != null)
            {
                return RedirectToAction("Details", "Appointments", new {userID = appointmentForm.OwnerId, id = newAppointment.AppointmentID });
            }
            else
            {
                User sitter = await api.Get<User>(id);
                User currentUser = await api.Get<User>(1);
                ViewBag.sitter = sitter;
                ViewBag.user = currentUser;
                return View("Form", appointmentForm);
            }
        }
    }
}
