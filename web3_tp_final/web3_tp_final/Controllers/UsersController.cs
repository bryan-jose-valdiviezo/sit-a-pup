using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using web3_tp_final.API;
using web3_tp_final.Controllers;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final
{
    public class UsersController : BaseController
    {
        private IEnumerable<User> _users;
        public UsersController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) :
            base(notificationUserHubContext, userConnectionManager, api)
        {
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            _users = await _api.Get<User>();

            return View(_users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var user = await _api.Get<User>(id);
            user.Appointments = await _api.GetAppointmentsForUser((int)id);
            user.AppointmentSitters = user.AppointmentAsSitter();
            
            if (user == null)
            {
                return NotFound();
            }
            if (GetCurrentUser() != null)
                ViewBag.CurrentID = GetCurrentUser().UserID;
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View("SignUp");
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName, Password,Email,Address,PhoneNumber")] User user)
        {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                user.UserID = 0;
                User createdUser = await _api.Post<User>(user);
                if (createdUser != null)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", new User { 
                        UserName = createdUser.UserName,
                        UserID = createdUser.UserID
                    });

                    return RedirectToAction("Index", "Home");
                }
            }

            return View("Signup");
        }

        [Route("Users/UsersDetails")]
        public async Task<IActionResult> UsersDetails()
        {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Login");

            var user = await _api.Get<User>(GetCurrentUser().UserID);
            return PartialView("_UserDetails", user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit()
        {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Login");

            var user = await _api.Get<User>(GetCurrentUser().UserID);
            return PartialView("_UserDetailsEdit", user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Email,Address,PhoneNumber")] User user)
        {
            if (GetCurrentUser() == null)
                return RedirectToAction("Index", "Login");

            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.UserID = id;
                    User updatedUser = await _api.Put<User>(id, user);
                    return PartialView("_UserDetails", updatedUser);
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
            }

            return View(user);
        }

        [HttpPost("Users/EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id, [Bind("UserName,Email,Address,PhoneNumber")] User user)
        {
            if (GetCurrentUser().UserID != id)
                return RedirectToAction("Index", "Home");

            User currentUser = await _api.Get<User>(id);
            user.UserID = id;
            user.Password = currentUser.Password;
            user.Status = currentUser.Status;

            var errors = new List<ValidationResult>();
            if (Validator.TryValidateObject(user, new ValidationContext(user), errors, true))
            {
                await _api.Put<User>(id, user);
                return PartialView("_UserDetails", user);
            }
            else
            {
                return PartialView("_UserDetails", currentUser);
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _api.Get<User>(id);
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
            await _api.Delete<User>(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _users.Any(e => e.UserID == id);
        }
    }
}
