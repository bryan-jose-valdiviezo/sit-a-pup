using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web3_tp_final.Data;
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
            //Attribue l'animal par défaut à l'utilisateur 1. //RÉCUPÉRER ID DE L'UTILISATEUR DANS SESSION
            User mockUser = await _context.Users.FindAsync(1);
            if (ModelState.IsValid)
            {
                var photo = Request.Form.Files.GetFile("photo");
                if (photo != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    await photo.CopyToAsync(memoryStream);
                    pet.Photo = memoryStream.ToArray();
                }        
                mockUser.Pets.Add(pet);
                _context.Users.Update(mockUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                try
                {
                    var photo = Request.Form.Files.GetFile("photo");
                    if (photo != null)
                    {
                    MemoryStream memoryStream = new MemoryStream();
                    await photo.CopyToAsync(memoryStream);
                    pet.Photo = memoryStream.ToArray();
                    }
                    User user = await _context.Users.FindAsync(1); //RÉCUPÉRER ID DE L'UTILISATEUR DANS SESSION
                    user.Pets.RemoveAll(petToRemove => petToRemove.PetID == pet.PetID); //Attention, si le mauvais ID de l'utilisateur est passé, un nouvel animal est créé et assigné à cet utilisateur.
                    user.Pets.Add(pet);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
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
