using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Data;
using web3_tp_final.Helpers;
using web3_tp_final.Models;
using static web3_tp_final.Models.Pet;

namespace web3_tp_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static APIController _aPIController;

        public HomeController(ILogger<HomeController> logger, APIController aPIController)
        {
            _aPIController = aPIController;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BecomeSitter()
        {
            return View();
        }

        public async Task<IActionResult> FindSitter()
        {
            IEnumerable<User> users = await _aPIController.Get<User>();
            return View(users);
        }

        public async Task<IActionResult> GenerateMockData()
        {
            List<Pet> pets1 = new List<Pet>();
            pets1.Add(new Pet { Name = "Skye", BirthYear = 2016, Specie = Species.CHIEN });
            User user1 = new User { UserName = "Quigid Trunslo", Password = "crosemont2021", Email = "trunslo@gmail.com", PhoneNumber = "514-250-2121", Address = "20e Avenue Montréal", Pets = pets1 };

            List<Pet> pets2 = new List<Pet>();
            pets2.Add(new Pet { Name = "Astro", BirthYear = 2017, Specie = Species.CHAT } );
            User user2 = new User { UserName = "Gertrev Framab", Password = "crosemont2021", Email = "framab@yahoo.fr", PhoneNumber = "514-250-2120", Address = "21e Avenue Montréal", Pets = pets2 };

            List<Pet> pets3 = new List<Pet>();
            pets3.Add(new Pet { Name = "Chase", BirthYear = 2018, Specie = Species.OISEAU });
            User user3 = new User { UserName = "Zoirile Wolvlowe", Password = "crosemont2021", Email = "zoirile@videotron.ca", PhoneNumber = "514-250-2119", Address = "22e Avenue Montréal", Pets = pets3 };

            List<Pet> pets4 = new List<Pet>();
            pets4.Add(new Pet { Name = "Render", BirthYear = 2019, Specie = Species.RONGEUR });
            User user4 = new User { UserName = "Sophhai Rusell", Password = "crosemont2021", Email = "rusell@ciuss.qc.ca", PhoneNumber = "514-250-2118", Address = "23e Avenue Montréal", Pets = pets4 };

            await _aPIController.Post<User>(user1);
            await _aPIController.Post<User>(user2);
            await _aPIController.Post<User>(user3);
            await _aPIController.Post<User>(user4);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GenerateMockLogin(int userID)
        {
            User mockUser = await _aPIController.Get<User>(userID);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "user", mockUser);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
