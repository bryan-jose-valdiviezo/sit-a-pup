using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using web3_tp_final.Data;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class LoginController : Controller
    {
        private readonly SitAPupContext _context;
        public LoginController(SitAPupContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogIn(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == username && m.Password == password);
            if (user != null)
            {
                ViewBag.UserId = user.UserID;//test affichage html
                //ajouter le id à la session
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Home", user);
        }
    }
}
