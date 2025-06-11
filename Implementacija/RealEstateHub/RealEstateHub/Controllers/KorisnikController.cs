using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateHub.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateHub.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public KorisnikController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Korisnik
        public IActionResult Index()
        {
            var korisnici = _userManager.Users.ToList();
            return View(korisnici);
        }

        // GET: Korisnik/Details/id
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var korisnik = await _userManager.FindByIdAsync(id);
            if (korisnik == null)
                return NotFound();

            return View(korisnik);
        }

        // GET: Korisnik/Edit/id
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var korisnik = await _userManager.FindByIdAsync(id);
            if (korisnik == null)
                return NotFound();

            return View(korisnik);
        }

        // POST: Korisnik/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id, ApplicationUser korisnik)
        {
            if (id != korisnik.Id)
                return NotFound();

            var korisnikIzBaze = await _userManager.FindByIdAsync(id);
            if (korisnikIzBaze == null)
                return NotFound();

            korisnikIzBaze.FirstName = korisnik.FirstName;
            korisnikIzBaze.LastName = korisnik.LastName;
            korisnikIzBaze.Email = korisnik.Email;
            korisnikIzBaze.PhoneNumber = korisnik.PhoneNumber;

            var result = await _userManager.UpdateAsync(korisnikIzBaze);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(korisnik);
        }

        // GET: Korisnik/Delete/id
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var korisnik = await _userManager.FindByIdAsync(id);
            if (korisnik == null)
                return NotFound();

            return View(korisnik);
        }

        // POST: Korisnik/DeleteConfirmed/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korisnik = await _userManager.FindByIdAsync(id);
            if (korisnik == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(korisnik);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(korisnik);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return Redirect("~/Identity/Account/Register");
        }
    }
}
