using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.DTO;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class FindSitterController : BaseController
    {
        public FindSitterController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) :
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await _api.GetUsersWithAppointments();

            if (GetCurrentUser() != null)
                users = users.Where(user => user.UserID != GetCurrentUser().UserID);

            //if (users == null)
            //    Debug.WriteLine("Error in fetching objects");
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> FilterByAvailability(DateTime? startDate, DateTime? endDate)
        {
            IEnumerable<User> availableSitters;

            if (startDate != null || endDate != null)
            {
                availableSitters = await _api.GetAvailableSittersForDate((DateTime)startDate, (DateTime)endDate);
            }
            else
            {
                availableSitters = await _api.GetUsersWithAppointments();
            }

            if (GetCurrentUser() != null)
                availableSitters = availableSitters.Where(user => user.UserID != GetCurrentUser().UserID);

            return PartialView("_AvailableSittersList", availableSitters);
        }

        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> BookAppointment(int id)
        {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Login");

            User sitter = await _api.Get<User>(id);
            User currentUser = await _api.Get<User>(GetCurrentUser().UserID);
            ViewBag.sitter = sitter;
            ViewBag.user = currentUser;
            return PartialView("_Form");
        }

        [HttpPost]
        [Route("FindSitter/{id}/BookAppointment")]
        public async Task<IActionResult> Create(int id, [Bind("AppointmentID, OwnerId, SitterId, StartDate, EndDate, PetIds")] AppointmentDTO appointmentForm)
        {
            if (GetCurrentUser() == null)
                RedirectToAction("Index", "Login");

            if (appointmentForm.OwnerId != null && appointmentForm.OwnerId == GetCurrentUser().UserID && appointmentForm.SitterId != null && appointmentForm.SitterId == id)
            {
                //Debug.WriteLine("Passed conditionnal, posting to api");
                Appointment newAppointment = await _api.PostAppointment(appointmentForm);
                if (newAppointment != null)
                {
                    SendNewAppointmentNotification(appointmentForm.SitterId, newAppointment.AppointmentID);
                    return RedirectToAction("Details", "Appointments", new { userID = GetCurrentUser().UserID, id = newAppointment.AppointmentID });
                }
            }

            return RedirectToAction("BookAppointment", "FindSitter", new { id = id });
        }
    }
}
