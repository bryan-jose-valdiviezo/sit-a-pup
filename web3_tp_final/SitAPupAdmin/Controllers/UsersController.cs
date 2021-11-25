using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitAPupAdmin.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult RechercherUser()
        {
            return View();
        }

        public IActionResult ModifierUser()
        {
            return View();
        }

        public IActionResult SupprimerUser()
        {
            return View();
        }

        public IActionResult VoirUsers()
        {
            return View();
        }
    }
}
