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
    public class AppointmentsController : BaseController
    {
        [Route("Users/{userID}/Appointments")]
        public async Task<IActionResult> Index(int userID)
        {
            if (CurrentUser() == null)
                return RedirectToAction("Index", "Home");

            ViewBag.CurrentUserID = CurrentUser().UserID;
            List<Appointment> appointments = await Api().GetAppointmentsForUser(CurrentUser().UserID);
            return View(appointments);
        }

        [Route("Users/{userID}/Appointments/{id}")]
        public async Task<IActionResult> Details(int userID, int? id)
        {

            if (CurrentUser() == null || CurrentUser().UserID != userID)
                return RedirectToAction("Index", "Home");


            Appointment appointment = await Api().Get<Appointment>(id);

            if (CurrentUser().UserID != appointment.Owner.UserID && CurrentUser().UserID != appointment.Sitter.UserID)
                return RedirectToAction("Index", "Home");

            appointment.Pets = await Api().GetPetsForAppointment((int)id);
            ViewBag.CurrentUser = CurrentUser();
            return View(appointment);
        }

        [HttpPost]
        [Route("Users/{userID}/Appointments/{id}/Review")]
        public async Task<IActionResult> PostReview(int userID, [Bind("AppointmentId, UserId, Stars, Comment")] ReviewDTO review)
        {
            Debug.WriteLine("Form appointment id: " + review.AppointmentId);
            Debug.WriteLine("Form UsedId:" + review.UserId);
            Debug.WriteLine("Form Stars:" + review.Stars);
            Debug.WriteLine("Form Comment:" + review.Comment);
            Review postedReview = await Api().PostReview(review);

            return RedirectToAction("Details", "Appointments", new { userID = review.UserId, id = review.AppointmentId });
        }
    }
}
