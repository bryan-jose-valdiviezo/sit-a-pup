using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using web3_tp_final.API;
using web3_tp_final.Helpers;
using web3_tp_final.Hubs;
using web3_tp_final.Interface;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class PetsController : BaseController
    {
        public PetsController(IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, APIController api) : 
            base(notificationUserHubContext,userConnectionManager, api)
        {
        }

        public async Task<IActionResult> Index()
        {
            int userID = 0;
            User user = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
            if (user != null)
            {
                userID = user.UserID;
            }

            List<Pet> pets = (List<Pet>)await _api.Get<Pet>();

            return View(pets.FindAll(pet => pet.UserID == userID));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Pet> pets = (List<Pet>)await _api.Get<Pet>();
            Pet pet = pets.Find(pet => pet.PetID == id);
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
        public async Task<IActionResult> Create([Bind("PetID,Name,Specie,BirthYear,Photo,UserID")] Pet pet)
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
                    pet.UserID = user.UserID;
                    await _api.Post<Pet>(pet);
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

            List<Pet> pets = (List<Pet>)await _api.Get<Pet>();
            Pet pet = pets.Find(pet => pet.PetID == id);

            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetID,Name,Specie,BirthYear,Photo,UserID")] Pet pet)
        {
            //Debug.WriteLine(pet.Photo.ToString());
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
                        } else
                        {
                            Pet databasePet =  await _api.Get<Pet>(id);
                            pet.Photo = databasePet.Photo;
                        }
                        pet.UserID = user.UserID;
                        await _api.Put<Pet>(id, pet);
                    } else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Pet> pets = (List<Pet>)await _api.Get<Pet>();
            Pet pet = pets.Find(pet => pet.PetID == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _api.Delete<Pet>(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
