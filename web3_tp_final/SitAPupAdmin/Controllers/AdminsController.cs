using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitAPupAdmin.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult AjoutAdmin()
        {
            return View();
        }

        public IActionResult ModifierAdmin()
        {
            return View();
        }

        public IActionResult SupprimerAdmin()
        {
            return View();
        }

        public IActionResult VoirAdmins()
        {
            return View();
        }
    }
}
