using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.DTO;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers.Users
{
    public class AppointmentsController : BaseController
    {
        public AppointmentsController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) :
            base(notificationUserHubContext, userConnectionManager, api)
        { 
        }

        [Route("Users/{userID}/Appointments")]
        public async Task<IActionResult> Index(int userID)
        {
            if (GetCurrentUser() == null || GetCurrentUser().UserID != userID)
                return RedirectToAction("Index", "Login");

            ViewBag.CurrentUserID = userID;
            List<Appointment> appointments = await _api.GetAppointmentsForUser(userID);
            ViewBag.AppointmentsAsOwner = appointments.Where(e => e.Owner.UserID == GetCurrentUser().UserID);
            ViewBag.AppointmentsAsSitter = appointments.Where(e => e.Sitter.UserID == GetCurrentUser().UserID);

            return View(appointments);
        }

        [Route("Users/{userID}/Appointments/{id}")]
        public async Task<IActionResult> Details(int userID, int? id)
        {
            if (GetCurrentUser() == null || GetCurrentUser().UserID != userID)
                return RedirectToAction("Index", "Login");

            Appointment appointment = await _api.Get<Appointment>(id);

            if (GetCurrentUser().UserID != appointment.Owner.UserID && GetCurrentUser().UserID != appointment.Sitter.UserID)
                return RedirectToAction("Index", "Home");

            appointment.Pets = await _api.GetPetsForAppointment((int)id);
            ViewBag.CurrentUser = GetCurrentUser();
            return View(appointment);
        }

        [Route("Users/{userID}/Appointments/{id}/UpdateAppointmentStatus")]
        public async Task<IActionResult> UpdateAppointmentStatus(int userID, int id, string? newStatus)
        {
            if (GetCurrentUser() == null || GetCurrentUser().UserID != userID)
                return RedirectToAction("Index", "Login");

            await _api.UpdateAppointmentStatus(id, newStatus);
            Appointment appointment = await _api.Get<Appointment>(id);
            return PartialView("_appointment_details_footer", appointment);
        }

        [HttpPost]
        [Route("Users/{userID}/Appointments/{id}/Review")]
        public async Task<IActionResult> PostReview(int userID, int id, [Bind("AppointmentId, UserId, Stars, Comment")] ReviewDTO review)
        {
            if (GetCurrentUser() == null || GetCurrentUser().UserID != userID)
                return RedirectToAction("Index", "Login");

            Review postedReview = await _api.PostReview(review);
            if (postedReview == null)
            {
                return RedirectToAction("Details", "Appointments", new { userID, id });
            }

            return RedirectToAction("Details", "Appointments", new { userID = review.UserId, id = review.AppointmentId });
        }
    }
}
