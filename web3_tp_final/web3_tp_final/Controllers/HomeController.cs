using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Data;
using web3_tp_final.Models;
using static web3_tp_final.Models.Pet;

namespace web3_tp_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SitAPupContext _sitAPupContext;
        private APIController api; 

        public HomeController(ILogger<HomeController> logger, SitAPupContext sitAPupContext)
        {
            api = new APIController();
            _sitAPupContext = sitAPupContext;
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
            IEnumerable<User> users = await api.Get<User>();
            return View(users);
        }

        public async Task<IActionResult> GenerateMockUsers()
        {
            List<Pet> pets1 = new List<Pet>();
            pets1.Add(new Pet { Name = "Skye", BirthYear = 2016, Specie = Species.CHIEN });
            _sitAPupContext.Add(new User { UserName = "Quigid Trunslo", Email = "trunslo@gmail.com", PhoneNumber = "514-250-2121", Address = "20e Avenue Montréal", Pets = pets1 } );

            List<Pet> pets2 = new List<Pet>();
            pets2.Add(new Pet { Name = "Astro", BirthYear = 2017, Specie = Species.CHAT } );
            _sitAPupContext.Add(new User { UserName = "Gertrev Framab", Email = "framab@yahoo.fr", PhoneNumber = "514-250-2120", Address = "21e Avenue Montréal", Pets = pets2 });

            List<Pet> pets3 = new List<Pet>();
            pets3.Add(new Pet { Name = "Chase", BirthYear = 2018, Specie = Species.OISEAU });
            _sitAPupContext.Add(new User { UserName = "Zoirile Wolvlowe", Email = "zoirile@videotron.ca", PhoneNumber = "514-250-2119", Address = "22e Avenue Montréal", Pets = pets3 });

            List<Pet> pets4 = new List<Pet>();
            pets4.Add(new Pet { Name = "Render", BirthYear = 2019, Specie = Species.RONGEUR });
            _sitAPupContext.Add(new User { UserName = "Sophhai Rusell", Email = "rusell@ciuss.qc.ca", PhoneNumber = "514-250-2118", Address = "23e Avenue Montréal", Pets = pets4 });

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
