using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateHub.Data;
using RealEstateHub.Models;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nekretnina.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,naslov,opisNekretnine,cijena,kvadratura,lokacija,brojSoba,vrstaNekretnine")] Nekretnina nekretnina)
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
            if (nekretnina.VlasnikId != currentUserId)
            {
                return Forbid();
            }

            return View(nekretnina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,naslov,opisNekretnine,cijena,kvadratura,lokacija,brojSoba,vrstaNekretnine")] Nekretnina nekretnina)
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
            if (existingNekretnina.VlasnikId != currentUserId)
            {
                return Forbid();
            }

            nekretnina.VlasnikId = existingNekretnina.VlasnikId;

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
            if (nekretnina.VlasnikId != currentUserId)
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
            if (nekretnina.VlasnikId != currentUserId)
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

        private bool NekretninaExists(int id)
        {
            return _context.Nekretnina.Any(e => e.Id == id);
        }
    }
}