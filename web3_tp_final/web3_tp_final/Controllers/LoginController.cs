using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using web3_tp_final.API;
using web3_tp_final.Helpers;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class LoginController : BaseController
    {
        private static APIController _aPIController;

        public LoginController(APIController aPIController)
        {
            _aPIController = aPIController;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogIn(string username, string password)
        {
            //var user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == username && m.Password == password);
            var user = await _aPIController.LogIn(username, password);

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

        public IActionResult CreateAccount()
        {
            return RedirectToAction("Create", "Users");
        }
    }
}
