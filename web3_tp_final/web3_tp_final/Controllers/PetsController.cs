using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using web3_tp_final.API;
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
            if (CheckIfUserIsConnected())
            {
                int userID = GetCurrentUser().UserID;
                List<Pet> pets = (List<Pet>)await _api.Get<Pet>();
                return View(pets.FindAll(pet => pet.UserID == userID));
            }

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (CheckIfUserIsConnected())
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

            return RedirectToAction("Index", "Login");
        }

        public IActionResult Create()
        {
            if (CheckIfUserIsConnected())
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetID,Name,Specie,BirthYear,Photo,UserID")] Pet pet)
        {
            if (CheckIfUserIsConnected())
            {
                if (ModelState.IsValid)
                {
                    User user = GetCurrentUser();
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
                return View(pet);
            }

            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (CheckIfUserIsConnected())
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

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetID,Name,Specie,BirthYear,Photo,UserID")] Pet pet)
        {
            if (CheckIfUserIsConnected())
            {
                if (ModelState.IsValid)
                {
                    User user = GetCurrentUser();

                    var photo = Request.Form.Files.GetFile("photo");
                    if (photo != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        await photo.CopyToAsync(memoryStream);
                        pet.Photo = memoryStream.ToArray();
                    } else
                    {
                        Pet databasePet = await _api.Get<Pet>(id);
                        pet.Photo = databasePet.Photo;
                    }
                    pet.UserID = user.UserID;
                    await _api.Put<Pet>(id, pet);

                    return RedirectToAction(nameof(Index));
                }
                return View(pet);
            }
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (CheckIfUserIsConnected())
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

            return RedirectToAction("Index", "Login");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (CheckIfUserIsConnected())
            {
                await _api.Delete<Pet>(id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Login");
        }
    }
}
