using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateHub.Data;
using RealEstateHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateHub.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class Korisnik_NekretninaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Korisnik_NekretninaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Korisnik_Nekretnina
        public async Task<IActionResult> Index()
        {
            return View(await _context.Korisnik_Nekretnina.ToListAsync());
        }

        // GET: Korisnik_Nekretnina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik_Nekretnina = await _context.Korisnik_Nekretnina
                .FirstOrDefaultAsync(m => m.kn_id == id);
            if (korisnik_Nekretnina == null)
            {
                return NotFound();
            }

            return View(korisnik_Nekretnina);
        }

        // GET: Korisnik_Nekretnina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Korisnik_Nekretnina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("kn_id,nekretninaId,korisnikId")] Korisnik_Nekretnina korisnik_Nekretnina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korisnik_Nekretnina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korisnik_Nekretnina);
        }

        // GET: Korisnik_Nekretnina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik_Nekretnina = await _context.Korisnik_Nekretnina.FindAsync(id);
            if (korisnik_Nekretnina == null)
            {
                return NotFound();
            }
            return View(korisnik_Nekretnina);
        }

        // POST: Korisnik_Nekretnina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("kn_id,nekretninaId,korisnikId")] Korisnik_Nekretnina korisnik_Nekretnina)
        {
            if (id != korisnik_Nekretnina.kn_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnik_Nekretnina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Korisnik_NekretninaExists(korisnik_Nekretnina.kn_id))
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
            return View(korisnik_Nekretnina);
        }

        // GET: Korisnik_Nekretnina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik_Nekretnina = await _context.Korisnik_Nekretnina
                .FirstOrDefaultAsync(m => m.kn_id == id);
            if (korisnik_Nekretnina == null)
            {
                return NotFound();
            }

            return View(korisnik_Nekretnina);
        }

        // POST: Korisnik_Nekretnina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korisnik_Nekretnina = await _context.Korisnik_Nekretnina.FindAsync(id);
            if (korisnik_Nekretnina != null)
            {
                _context.Korisnik_Nekretnina.Remove(korisnik_Nekretnina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Korisnik_NekretninaExists(int id)
        {
            return _context.Korisnik_Nekretnina.Any(e => e.kn_id == id);
        }
    }
}
