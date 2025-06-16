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
    public class SesijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SesijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sesija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sesija.ToListAsync());
        }

        // GET: Sesija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesija = await _context.Sesija
                .FirstOrDefaultAsync(m => m.sesijaId == id);
            if (sesija == null)
            {
                return NotFound();
            }

            return View(sesija);
        }

        // GET: Sesija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sesija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sesijaId")] Sesija sesija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sesija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sesija);
        }

        // GET: Sesija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesija = await _context.Sesija.FindAsync(id);
            if (sesija == null)
            {
                return NotFound();
            }
            return View(sesija);
        }

        // POST: Sesija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("sesijaId")] Sesija sesija)
        {
            if (id != sesija.sesijaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesijaExists(sesija.sesijaId))
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
            return View(sesija);
        }

        // GET: Sesija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesija = await _context.Sesija
                .FirstOrDefaultAsync(m => m.sesijaId == id);
            if (sesija == null)
            {
                return NotFound();
            }

            return View(sesija);
        }

        // POST: Sesija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesija = await _context.Sesija.FindAsync(id);
            if (sesija != null)
            {
                _context.Sesija.Remove(sesija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesijaExists(int id)
        {
            return _context.Sesija.Any(e => e.sesijaId == id);
        }
    }
}
