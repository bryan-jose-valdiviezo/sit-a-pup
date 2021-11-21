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

namespace web3_tp_final.Controllers.Users
{
    [Route("Users/{userID}/[controller]")]
    public class AppointmentsController : Controller
    {
        private APIController api;

        public AppointmentsController()
        {
            api = new APIController();
        }

        public async Task<IActionResult> Index(int userID)
        {
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");

            if (user == null)
                return RedirectToAction("Index", "Home");

            ViewBag.CurrentUserID = user.UserID;
            List<Appointment> appointments = await api.GetAppointmentsForUser(user.UserID);
            return View(appointments);
        }

        [Route("{id}")]
        public async Task<IActionResult> Details(int userID, int? id)
        {
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");

            if (user == null || user.UserID != userID)
                return RedirectToAction("Index", "Home");

            user = await api.Get<User>(userID);

            Appointment appointment = await api.Get<Appointment>(id);

            appointment.Pets = await api.GetPetsForAppointment((int)id);

            ViewBag.user = user;
            return View(appointment);
        }

        [HttpPost]
        [Route("{id}/Review")]
        public async Task<IActionResult> PostReview(int userID, [Bind("AppointmentId, UserId, Stars, Comment")] ReviewDTO review)
        {
            Debug.WriteLine("Form appointment id: " + review.AppointmentId);
            Debug.WriteLine("Form UsedId:" + review.UserId);
            Debug.WriteLine("Form Stars:" + review.Stars);
            Debug.WriteLine("Form Comment:" + review.Comment);
            Review postedReview = await api.PostReview(review);

            return RedirectToAction("Details", "Appointments", new { userID = review.UserId, id = review.AppointmentId });
        }
    }
}
