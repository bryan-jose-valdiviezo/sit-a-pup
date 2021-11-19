using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web3_tp_final.API;
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

            List<Pet> pets = (List<Pet>)await _aPIController.Get<Pet>();

            return View(pets.FindAll(pet => pet.UserID == userID));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Pet> pets = (List<Pet>)await _aPIController.Get<Pet>();
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
        public async Task<IActionResult> Create([Bind("PetID,Name,SpecieString,BirthYear,Photo,UserID")] Pet pet)
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
                    await _aPIController.Post<Pet>(pet);
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

            List<Pet> pets = (List<Pet>)await _aPIController.Get<Pet>();
            Pet pet = pets.Find(pet => pet.PetID == id);

            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetID,Name,SpecieString,BirthYear,Photo,UserID")] Pet pet)
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
                        } else
                        {
                            Pet databasePet =  await _aPIController.Get<Pet>(id);
                            pet.Photo = databasePet.Photo;
                        }
                        pet.UserID = user.UserID;
                        await _aPIController.Put<Pet>(id, pet);
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

            List<Pet> pets = (List<Pet>)await _aPIController.Get<Pet>();
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
            await _aPIController.Delete<Pet>(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
