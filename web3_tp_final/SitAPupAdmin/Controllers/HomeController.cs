using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SitAPupAdmin.API;
using SitAPupAdmin.Helpers;
using SitAPupAdmin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SitAPupAdmin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private static APIController _aPIController;

        public HomeController(ILogger<HomeController> logger, APIController apiController)
        {
            _logger = logger;
            _aPIController = apiController;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> LogIn(string username, string password)
        {
            var admin = await _aPIController.LogIn(username, password);
            TempData["nonTrouve"] = "";
            if (admin != null)
            {
                Admin newAdmin = new Admin();
                newAdmin.Name = admin.Name;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "admin", newAdmin);
            }
            else
            {
                TempData["nonTrouve"] = "Cet administrateur n'existe pas.";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            //var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == username && m.Password == password);
            var user = SessionHelper.GetObjectFromJson<Admin>(HttpContext.Session, "admin");

            if (user != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
