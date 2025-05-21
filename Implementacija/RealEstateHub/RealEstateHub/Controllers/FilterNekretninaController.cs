using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateHub.Data;
using RealEstateHub.Models;

namespace RealEstateHub.Controllers
{
    public class FilterNekretninaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilterNekretninaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FilterNekretnina
        public async Task<IActionResult> Index()
        {
            return View(await _context.FilterNekretnina.ToListAsync());
        }

        // GET: FilterNekretnina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterNekretnina = await _context.FilterNekretnina
                .FirstOrDefaultAsync(m => m.filterNekretninaId == id);
            if (filterNekretnina == null)
            {
                return NotFound();
            }

            return View(filterNekretnina);
        }

        // GET: FilterNekretnina/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilterNekretnina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("filterNekretninaId,minCijena,maxCijena,brojSoba,kvadratura,tipNekretnine")] FilterNekretnina filterNekretnina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filterNekretnina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filterNekretnina);
        }

        // GET: FilterNekretnina/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterNekretnina = await _context.FilterNekretnina.FindAsync(id);
            if (filterNekretnina == null)
            {
                return NotFound();
            }
            return View(filterNekretnina);
        }

        // POST: FilterNekretnina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("filterNekretninaId,minCijena,maxCijena,brojSoba,kvadratura,tipNekretnine")] FilterNekretnina filterNekretnina)
        {
            if (id != filterNekretnina.filterNekretninaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filterNekretnina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilterNekretninaExists(filterNekretnina.filterNekretninaId))
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
            return View(filterNekretnina);
        }

        // GET: FilterNekretnina/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterNekretnina = await _context.FilterNekretnina
                .FirstOrDefaultAsync(m => m.filterNekretninaId == id);
            if (filterNekretnina == null)
            {
                return NotFound();
            }

            return View(filterNekretnina);
        }

        // POST: FilterNekretnina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filterNekretnina = await _context.FilterNekretnina.FindAsync(id);
            if (filterNekretnina != null)
            {
                _context.FilterNekretnina.Remove(filterNekretnina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilterNekretninaExists(int id)
        {
            return _context.FilterNekretnina.Any(e => e.filterNekretninaId == id);
        }
    }
}
