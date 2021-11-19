using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers.Users
{
    public class AppointmentsController : Controller
    {
        private APIController api;

        public AppointmentsController()
        {
            api = new APIController();
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("Users/{userID}/Appointments/{id}")]
        public async Task<IActionResult> Details(int userID, int id)
        {
            User user = await api.Get<User>(userID);
            Appointment appointment = await api.Get<Appointment>(id);
            appointment.Pets = await api.GetPetsForAppointment(id);

            ViewBag.user = user;
            return View(appointment);
        }
    }
}
