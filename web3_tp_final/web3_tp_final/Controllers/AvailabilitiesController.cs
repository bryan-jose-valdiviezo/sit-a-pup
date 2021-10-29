using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Controllers
{
    public class AvailabilitiesController : Controller
    {
        public IActionResult Index()
        {
            return View("Availabilities");
        }
    }
}
