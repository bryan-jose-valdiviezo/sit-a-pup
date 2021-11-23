using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web3_tp_final.API;
using web3_tp_final.Controllers;
using web3_tp_final.Helpers;
using web3_tp_final.Models;

namespace web3_tp_final
{
    public class UsersController : BaseController
    {
        private static APIController _aPIController;
        private IEnumerable<User> _users;

        public UsersController(APIController aPIController)
        {
            _aPIController = aPIController;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            _users = await _aPIController.Get<User>();

            return View(_users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _aPIController.Get<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            if (CurrentUser() != null)
                ViewBag.CurrentID = CurrentUser().UserID;
            return View("DetailsUser", user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View("SignUp");
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName, Password,Email,Address,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = 0;
                User createdUser = await _aPIController.Post<User>(user);
                if (createdUser != null)
                    return RedirectToAction("Index");
            }

            return View("Signup");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _aPIController.Get<User>(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Email,Address,PhoneNumber")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UserID = id;
                    User updatedUser = await _aPIController.Put<User>(id, user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _aPIController.Get<User>(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _aPIController.Delete<User>(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _users.Any(e => e.UserID == id);
        }
    }
}
