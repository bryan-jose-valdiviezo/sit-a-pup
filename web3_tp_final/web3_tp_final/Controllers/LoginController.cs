using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) : 
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogIn(string username, string password)
        {
            var user = await _api.LogIn(username, password);

            if (user != null)
            {
                User newUser = new User();
                newUser.UserID = user.UserID;
                newUser.UserName = user.UserName;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", newUser);
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            var user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");

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

        public IActionResult CreateAccount()
        {
            return RedirectToAction("Create", "Users");
        }
    }
}
