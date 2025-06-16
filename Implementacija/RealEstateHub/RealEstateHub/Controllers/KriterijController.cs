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
    public class KriterijController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KriterijController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kriterij
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kriterij.ToListAsync());
        }

        // GET: Kriterij/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kriterij = await _context.Kriterij
                .FirstOrDefaultAsync(m => m.kriterijId == id);
            if (kriterij == null)
            {
                return NotFound();
            }

            return View(kriterij);
        }

        // GET: Kriterij/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kriterij/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("kriterijId,obavjestenjeId,vrijednost")] Kriterij kriterij)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kriterij);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kriterij);
        }

        // GET: Kriterij/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kriterij = await _context.Kriterij.FindAsync(id);
            if (kriterij == null)
            {
                return NotFound();
            }
            return View(kriterij);
        }

        // POST: Kriterij/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("kriterijId,obavjestenjeId,vrijednost")] Kriterij kriterij)
        {
            if (id != kriterij.kriterijId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kriterij);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KriterijExists(kriterij.kriterijId))
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
            return View(kriterij);
        }

        // GET: Kriterij/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kriterij = await _context.Kriterij
                .FirstOrDefaultAsync(m => m.kriterijId == id);
            if (kriterij == null)
            {
                return NotFound();
            }

            return View(kriterij);
        }

        // POST: Kriterij/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kriterij = await _context.Kriterij.FindAsync(id);
            if (kriterij != null)
            {
                _context.Kriterij.Remove(kriterij);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KriterijExists(int id)
        {
            return _context.Kriterij.Any(e => e.kriterijId == id);
        }
    }
}
