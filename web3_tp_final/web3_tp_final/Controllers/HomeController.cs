using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.Data;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SitAPupContext _sitAPupContext;

        public HomeController(ILogger<HomeController> logger, SitAPupContext sitAPupContext)
        {
            _sitAPupContext = sitAPupContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GenerateMockUsers()
        {
            _sitAPupContext.Add(new User { UserName = "Quigid Trunslo", Email = "trunslo@gmail.com", PhoneNumber = "514-250-2121", Address = "20e Avenue Montréal" } );
            _sitAPupContext.Add(new User { UserName = "Gertrev Framab", Email = "framab@yahoo.fr", PhoneNumber = "514-250-2120", Address = "21e Avenue Montréal" });
            _sitAPupContext.Add(new User { UserName = "Zoirile Wolvlowe", Email = "zoirile@videotron.ca", PhoneNumber = "514-250-2119", Address = "22e Avenue Montréal" });
            _sitAPupContext.Add(new User { UserName = "Sophhai Rusell", Email = "rusell@ciuss.qc.ca", PhoneNumber = "514-250-2118", Address = "23e Avenue Montréal" });
            await _sitAPupContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
