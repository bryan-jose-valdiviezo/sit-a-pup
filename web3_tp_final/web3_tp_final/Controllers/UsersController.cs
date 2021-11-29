using System;
using System.Collections.Generic;
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
            if (CurrentUser() != null)
            {
                ViewBag.CurrentID = CurrentUser().UserID;
            }

            return View(user);
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
                User createdUser = await _api.Post<User>(user);
                if (createdUser != null)
                    return RedirectToAction("Index");
            }

            return View("Signup");
        }

        // GET: Users/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
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
            else
            {
                return View("Details", user);
            }            
        }*/

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Email,Address,PhoneNumber")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    user.UserID = id;
                    var currentUser = await _api.Get<User>(id);
                    user.Password = currentUser.Password;
                    await _api.Put<User>(id, user);                   
                    UpdateCurrentUser(await _api.Get<User>(id));
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
                //return RedirectToAction(nameof(Index));
            }
            return View("Details", user);
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
