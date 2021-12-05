using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
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
            List<Pet> pets1 = new List<Pet>();
            pets1.Add(new Pet { Name = "Skye", BirthYear = 2016, Specie = Species.CHIEN });
            User user1 = new User { UserName = "Quigid Trunslo", Password = "crosemont2021", Email = "trunslo@gmail.com", PhoneNumber = "514-250-2121", Address = "20e Avenue Montréal", Pets = pets1 };
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

            List <Pet> pets2 = new List<Pet>();
            pets2.Add(new Pet { Name = "Astro", BirthYear = 2017, Specie = Species.CHAT } );
            User user2 = new User { UserName = "Gertrev Framab", Password = "crosemont2021", Email = "framab@yahoo.fr", PhoneNumber = "514-250-2120", Address = "21e Avenue Montréal", Pets = pets2 };

            List<Pet> pets3 = new List<Pet>();
            pets3.Add(new Pet { Name = "Chase", BirthYear = 2018, Specie = Species.OISEAU });
            User user3 = new User { UserName = "Zoirile Wolvlowe", Password = "crosemont2021", Email = "zoirile@videotron.ca", PhoneNumber = "514-250-2119", Address = "22e Avenue Montréal", Pets = pets3 };

            List<Pet> pets4 = new List<Pet>();
            pets4.Add(new Pet { Name = "Render", BirthYear = 2019, Specie = Species.RONGEUR });
            User user4 = new User { UserName = "Sophhai Rusell", Password = "crosemont2021", Email = "rusell@ciuss.qc.ca", PhoneNumber = "514-250-2118", Address = "23e Avenue Montréal", Pets = pets4 };

            await _api.Post<User>(user1);
            await _api.Post<User>(user2);
            await _api.Post<User>(user3);
            await _api.Post<User>(user4);

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
