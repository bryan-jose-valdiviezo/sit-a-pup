using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web3_tp_final.Data;
using web3_tp_final.Helpers;
using web3_tp_final.Models;

namespace web3_tp_final.Controllers
{
    public class PetsController : Controller
    {
        private readonly SitAPupContext _context;

        public PetsController(SitAPupContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Pets.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.PetID == id);
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
                    user.Pets.Add(pet);
                    _context.Users.Update(user);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } 
                else
                {
                    //Données du formulaires pourraient être sauvegardées temporairement
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

            var pet = await _context.Pets.FindAsync(id);
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
                        if (photo != null)
                        {
                            MemoryStream memoryStream = new MemoryStream();
                            await photo.CopyToAsync(memoryStream);
                            pet.Photo = memoryStream.ToArray();
                        }
                        user.Pets.RemoveAll(petToRemove => petToRemove.PetID == id); //Problème: le update ne semblait pas fonctionner, alors je fais un remplacement (CM)
                        user.Pets.Add(pet);
                        _context.Update(user);
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "user", user);
                        await _context.SaveChangesAsync();
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .FirstOrDefaultAsync(m => m.PetID == id);
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
            var pet = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GenerateMockPets()
        {
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.PetID == id);
        }
    }
}
