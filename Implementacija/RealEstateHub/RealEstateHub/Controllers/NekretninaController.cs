using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateHub.Data;
using RealEstateHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateHub.Controllers
{
    public class NekretninaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NekretninaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["CijenaSort"] = String.IsNullOrEmpty(sortOrder) ? "cijena_desc" : "";
            ViewData["KvadraturaSort"] = sortOrder == "kvadratura" ? "kvadratura_desc" : "kvadratura";
            ViewData["SobeSort"] = sortOrder == "sobe" ? "sobe_desc" : "sobe";

            var nekretnine = from n in _context.Nekretnina select n;

            switch (sortOrder)
            {
                case "cijena_desc":
                    nekretnine = nekretnine.OrderByDescending(n => n.cijena);
                    break;
                case "kvadratura":
                    nekretnine = nekretnine.OrderBy(n => n.kvadratura);
                    break;
                case "kvadratura_desc":
                    nekretnine = nekretnine.OrderByDescending(n => n.kvadratura);
                    break;
                case "sobe":
                    nekretnine = nekretnine.OrderBy(n => n.brojSoba);
                    break;
                case "sobe_desc":
                    nekretnine = nekretnine.OrderByDescending(n => n.brojSoba);
                    break;
                default:
                    nekretnine = nekretnine.OrderBy(n => n.cijena); // defaultno sortiranje po cijeni rastuće
                    break;
            }

            return View(await nekretnine.ToListAsync());
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nekretnina = await _context.Nekretnina
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            return View(nekretnina);
        }

        [Authorize(Roles = "Korisnik, Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Create(
            [Bind("Id,naslov,opisNekretnine,cijena,kvadratura,lokacija,brojSoba,vrstaNekretnine,Slika")] Nekretnina nekretnina)
        {
            ModelState.Remove("VlasnikId");
            ModelState.Remove("Vlasnik");

            if (ModelState.IsValid)
            {
                nekretnina.VlasnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Add(nekretnina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nekretnina);
        }


        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nekretnina = await _context.Nekretnina.FindAsync(id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (nekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            return View(nekretnina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,naslov,opisNekretnine,cijena,kvadratura,lokacija,brojSoba,vrstaNekretnine,Slika")] Nekretnina nekretnina)
        {
            ModelState.Remove("VlasnikId");
            ModelState.Remove("Vlasnik");

            if (id != nekretnina.Id)
            {
                return NotFound();
            }

            var existingNekretnina = await _context.Nekretnina.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            if (existingNekretnina == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (existingNekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            nekretnina.VlasnikId = existingNekretnina.VlasnikId;

            if (string.IsNullOrEmpty(nekretnina.Slika))
            {
                nekretnina.Slika = existingNekretnina.Slika;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nekretnina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NekretninaExists(nekretnina.Id))
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
            return View(nekretnina);
        }

        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nekretnina = await _context.Nekretnina
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (nekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            return View(nekretnina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nekretnina = await _context.Nekretnina.FindAsync(id);

            if (nekretnina == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (nekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            if (nekretnina != null)
            {
                _context.Nekretnina.Remove(nekretnina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult Pretraga()
        {
            return View(new FilterNekretnina());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Pretraga(FilterNekretnina filter)
        {
            var query = _context.Nekretnina.AsQueryable();

            if (filter.minCijena > 0)
                query = query.Where(n => n.cijena >= filter.minCijena);

            if (filter.maxCijena > 0)
                query = query.Where(n => n.cijena <= filter.maxCijena);

            if (filter.minBrojSoba > 0)
                query = query.Where(n => n.brojSoba >= filter.minBrojSoba);

            if (filter.maxBrojSoba > 0)
                query = query.Where(n => n.brojSoba <= filter.maxBrojSoba);

            if (filter.minKvadratura > 0)
                query = query.Where(n => n.kvadratura >= filter.minKvadratura);

            if (filter.maxKvadratura > 0)
                query = query.Where(n => n.kvadratura <= filter.maxKvadratura);
          
            query = query.Where(n => n.vrstaNekretnine == filter.tipNekretnine);

            var rezultat = await query.ToListAsync();

            if (!rezultat.Any())
            {
                ViewBag.Poruka = "Nema nekretnina koje zadovoljavaju kriterije.";
            }

            return View("RezultatiPretrage", rezultat);
        }



        private bool NekretninaExists(int id)
        {
            return _context.Nekretnina.Any(e => e.Id == id);
        }
    }
}