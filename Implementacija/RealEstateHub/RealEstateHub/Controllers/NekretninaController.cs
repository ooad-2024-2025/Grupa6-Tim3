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

namespace RealEstateHub.Controllers{
    public class NekretninaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NekretninaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nekretnina
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nekretnina.ToListAsync());
        }

        // GET: Nekretnina/Details/5
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

        // GET: Nekretnina/Create
        [Authorize(Roles = "Korisnik")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nekretnina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Create([Bind("Id,naslov,opisNekretnine,cijena,kvadratura,lokacija,brojSoba,vrstaNekretnine")] Nekretnina nekretnina)

        {
            if (ModelState.IsValid){
                // Postavi VlasnikId iz prijavljenog korisnika
                nekretnina.VlasnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Add(nekretnina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nekretnina);
        }

        // GET: Nekretnina/Edit/5
        [Authorize(Roles = "Korisnik")]
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
            return View(nekretnina);
        }

        // POST: Nekretnina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,naslov,opisNekretnine,cijena,kvadratura,lokacija,brojSoba,vlasnikId,vrstaNekretnine")] Nekretnina nekretnina)
        {
            if (id != nekretnina.Id)
            {
                return NotFound();
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

        // GET: Nekretnina/Delete/5
        [Authorize(Roles = "Korisnik")]
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

            return View(nekretnina);
        }

        // POST: Nekretnina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nekretnina = await _context.Nekretnina.FindAsync(id);
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
