using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;
using static web3_tp_final.Models.Pet;

namespace web3_tp_final.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
                              IHubContext<NotificationUserHub> notificationUserHubContext,
                              IUserConnectionManager userConnectionManager,
                              APIController api) : 
                                  base(notificationUserHubContext,
                                       userConnectionManager,
                                       api)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FindSitter()
        {
            IEnumerable<User> users = await _api.Get<User>();
            return View(users);
        }

        public async Task<IActionResult> GenerateMockData()
        {
            Random random = new Random();
            List<Pet> pets = new List<Pet>();
            List<User> users = new List<User>();
            List<Availability> availabilities = new List<Availability>();
            string[] usernames = new string[40] { "Coltoliv Fermed", "Leonevan Galpdor", "Davtari Babalb", "Krikol Bachtrul","Marsab Scriloa","Noemat Racygatw","Tommjoa Colgin","Aubarma Harhase","Yadibren Vaunort","Jayleze Stinray","Chrhay Gatldarc","Mia jal Ligdobk","Elianad Veager","Savacarr Shatod","Carllog Wooamos","Denstac Beenstur","Halebere Boldduf","Catacal Windlync","Brejam Lawloc","Eveama Huttild", "Rarnam Danimaa", "Olsmula Dron", "Chruvlith Grar", "Sizzalull Girgix", "Camath Brigdella", "Ievlag Grovasaarr", "Kades Gagroggorm", "Lurwos Pedatint", "Chreth Birgossus", "Lugrog Glonsinn", "Shuglah Todditha", "Shir Nurm", "Jen Pastellant", "Neir Burgal", "Gassa Glan", "Mano Drotholen", "Deh Drogderre", "Messo Bremarerm", "Zorshe Stytara", "Kot Forbarru" };
            string[] petNames = new string[50] { "Sharptooth", "Kaleida", "Balgzr", "Thallophyta", "Mothball", "Athugz", "Irubleu", "Render", "Hyphae", "Star", "Awin", "Destiny", "RleSothoorla", "Kokoro", "Oreo", "Tlaitparh", "Mist", "Bluestar", "Squeekers", "Athugz", "Imamu", "Wendigo", "Madare", "Wendigo", "Ybhugr", "Raymundo", "Ynremr", "MrGlows", "Cibarius", "Saka", "Garth", "Misty", "Oasis", "Dawn", "Goro", "Pinocchio", "Chub", "Gus", "Sebastian", "Geppetto", "Pretzel", "Camay", "Muse", "Clue", "Bolt", "Squeek", "Orchid", "Smoke", "Chico", "Rain" };
            string email = "courriel@crosemont.qc.ca";
            string password = "crosemont2021";
            string phoneNumber = "514-376-1620";
            string address = "6400, 16e Avenue Montréal(Québec) H1X 2S9";

            for (int i = 0; i < 20; i++)
            {
                availabilities.Add(new Availability() { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(random.Next(7,72)) });
                pets.Add(new Pet() { Name = petNames[random.Next(petNames.Length)], BirthYear = random.Next(2008,2021), Specie = Species.CHIEN });
                pets.Add(new Pet() { Name = petNames[random.Next(petNames.Length)], BirthYear = random.Next(2008, 2021), Specie = Species.CHIEN });
                await _api.Post<User>(new User()
                {
                    UserName = usernames[random.Next(usernames.Length)],
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Pets = pets,
                    Availabilities = availabilities
                });
                availabilities.Clear();
                pets.Clear();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GenerateMockLogin(int userID)
        {
            User mockUser = await _api.Get<User>(userID);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "user", mockUser);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> SendToSpecificUser(int userId)
        {
            var connections = _userConnectionManager.GetUserConnections(userId.ToString());
            //Debug.WriteLine("Sending to user : " + userId);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendNewFormToUser", 5);
                }
            }
            //else
            //{
            //    Debug.WriteLine("Connection not found, sending to nobody");
            //}
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SendToAllUsers()
        {
            await _notificationUserHubContext.Clients.All.SendAsync("sendToAll", "Heres my message to all");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetNotification(int appointmentId)
        {
            Appointment appointment = await _api.Get<Appointment>(appointmentId);

            return PartialView("_ToastNotification", appointment);
        }
    }
}


/*List<Appointment> appointments= new List<Appointment>();            
for (int i = 0; i < 10; i++)
{
    Appointment appointment = new Appointment();
    appointment.Reviews = new List<Review>();
    User owner = new User { UserName = "User " + i, Password = "crosemont2021", Email = "trunslo" + i + "@gmail.com", PhoneNumber = "514-25" + i + "-2121", Address = "20e Avenue Montréal"};

    Review review = new Review();
    review.Stars = i;
    review.Comment = "Test " + i;
    appointment.Owner = owner;
    appointment.Reviews.Add(review);

    appointments.Add(appointment);
}
user1.AppointmentSitters = appointments;*/