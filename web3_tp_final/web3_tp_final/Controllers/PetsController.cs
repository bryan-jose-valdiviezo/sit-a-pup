using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web3_tp_final.API;
using web3_tp_final.Data;
using web3_tp_final.Helpers;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class PetsController : Controller
    {
        private static APIController _aPIController;

        public PetsController(APIController aPIController)
        {
            _aPIController = aPIController;
        }

        public async Task<IActionResult> Index()
        {
            int userID = 0;
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
            if (user != null)
            {
                userID = user.UserID;
            }

            user = await _aPIController.Get<User>(userID);

            return View(user.Pets);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int userID = 0;
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
            if (user != null)
            {
                userID = user.UserID;
            }

            user = await _aPIController.Get<User>(userID);

            var pet = user.Pets.Find(pet => pet.PetID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetID,Name,SpecieString,BirthYear,Photo,IsBeingSitted,Sitter,SittingStart,SittingEnd,UserID")] Pet pet)
        {
            if (ModelState.IsValid)
                {
                User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
                if (user != null)
                {
                    var photo = Request.Form.Files.GetFile("photo");
                    if (photo != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        await photo.CopyToAsync(memoryStream);
                        pet.Photo = memoryStream.ToArray();
                    }
                    
                    User userFromDB = await _aPIController.Get<User>(user.UserID);
                    userFromDB.Pets.Add(pet);
                    await _aPIController.Put<User>(user.UserID, userFromDB);
                    return RedirectToAction(nameof(Index));
                } 
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(pet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int userID = 0;
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
            if (user != null)
            {
                userID = user.UserID;
            }

            user = await _aPIController.Get<User>(userID);

            var pet = user.Pets.Find(pet => pet.PetID == id);

            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetID,Name,SpecieString,BirthYear,Photo,IsBeingSitted,Sitter,SittingStart,SittingEnd,UserID")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
                try
                {
                    if (user != null)
                    {
                        var photo = Request.Form.Files.GetFile("photo");
                        if (photo != null && photo.Length > 0)
                        {
                            MemoryStream memoryStream = new MemoryStream();
                            await photo.CopyToAsync(memoryStream);
                            pet.Photo = memoryStream.ToArray();
                        }
                        //Tour de passe passe à modifier
                        pet.UserID = user.UserID;
                        User userFromDb = await _aPIController.Get<User>(user.UserID);
                        userFromDb.Pets.RemoveAll(p => p.PetID == pet.PetID);
                        await _aPIController.Put<User>(user.UserID, userFromDb);
                    } else
                    {
                        //Données du formulaires pourraient être sauvegardées temporairement
                        return RedirectToAction("Index", "Login");
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetID))
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
            return View(pet);
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var pet = await _context.Pets
        //        .FirstOrDefaultAsync(m => m.PetID == id);
        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(pet);
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var pet = await _context.Pets.FindAsync(id);
        //    _context.Pets.Remove(pet);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PetExists(int id)
        {
            //return _context.Pets.Any(e => e.PetID == id);
            return false;
        }
    }
}
