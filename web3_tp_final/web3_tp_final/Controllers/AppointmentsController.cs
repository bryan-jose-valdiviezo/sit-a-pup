using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class AppointmentsController : BaseController
    {
        private static APIController _aPIController;

        public AppointmentsController(APIController aPIController)
        {
            _aPIController = aPIController;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            Appointment appointment = await _aPIController.Get<Appointment>(id);

            return View(appointment);
        }
    }
}
