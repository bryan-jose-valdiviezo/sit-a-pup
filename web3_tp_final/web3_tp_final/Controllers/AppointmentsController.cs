using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
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

        public async Task<IActionResult> Details(int? id)
        {
            Appointment appointment = await api.Get<Appointment>(id);

            return View(appointment);
        }
    }
}
