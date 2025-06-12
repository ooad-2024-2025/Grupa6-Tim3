﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Index(string sortOrder, int page = 1, int pageSize = 6)
        {
            ViewData["CijenaSort"] = String.IsNullOrEmpty(sortOrder) ? "cijena_desc" : "";
            ViewData["KvadraturaSort"] = sortOrder == "kvadratura" ? "kvadratura_desc" : "kvadratura";
            ViewData["SobeSort"] = sortOrder == "sobe" ? "sobe_desc" : "sobe";

            var nekretnine = from n in _context.Nekretnina select n;

            // Sortiranje
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
                    nekretnine = nekretnine.OrderBy(n => n.cijena);
                    break;
            }

            // Paginacija
            var totalItems = await nekretnine.CountAsync();
            var items = await nekretnine
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new PagedNekretnineViewModel
            {
                Nekretnine = items,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                SortOrder = sortOrder
            };

            return View(viewModel);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var nekretnina = await _context.Nekretnina
                .Include(n => n.Lokacija)
                .Include(n => n.Vlasnik)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (nekretnina == null)
                return NotFound();

            var currentUserId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
            bool isOwner = (currentUserId != null) && (nekretnina.VlasnikId == currentUserId);

            // Povećaj broj pregleda SAMO ako nije vlasnik
            if (!isOwner)
            {
                nekretnina.BrojPregleda++;
                await _context.SaveChangesAsync();
            }

            // Prikaz broja pregleda svima (ili možeš i ovdje po želji promijeniti)
            ViewBag.BrojPregleda = isOwner ? nekretnina.BrojPregleda : (int?)null;

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
        public async Task<IActionResult> Create(Nekretnina nekretnina)
        {
            // Uklonimo Vlasnik koji se dodjeljuje automatski
            ModelState.Remove("Vlasnik");
            ModelState.Remove("VlasnikId");

            // Ako forma nije ispravna, vrati prikaz sa greškama
            if (!ModelState.IsValid)
            {

                return View(nekretnina);
            }

            // Dodjeljujemo trenutno prijavljenog korisnika kao vlasnika
            nekretnina.VlasnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Dodavanje u bazu
            _context.Add(nekretnina);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nekretnina = await _context.Nekretnina
                .Include(n => n.Lokacija)
                .FirstOrDefaultAsync(n => n.Id == id);

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
        public async Task<IActionResult> Edit(int id, Nekretnina nekretnina)
        {
            ModelState.Remove("VlasnikId");
            ModelState.Remove("Vlasnik");

            if (id != nekretnina.Id)
            {
                return NotFound();
            }

            // Učitaj postojeću nekretninu sa Lokacijom
            var existingNekretnina = await _context.Nekretnina
                .Include(n => n.Lokacija)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (existingNekretnina == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (existingNekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
            {
                return Forbid();
            }

            // Ažuriraj polja nekretnine
            existingNekretnina.naslov = nekretnina.naslov;
            existingNekretnina.opisNekretnine = nekretnina.opisNekretnine;
            existingNekretnina.cijena = nekretnina.cijena;
            existingNekretnina.kvadratura = nekretnina.kvadratura;
            existingNekretnina.brojSoba = nekretnina.brojSoba;
            existingNekretnina.vrstaNekretnine = nekretnina.vrstaNekretnine;

            if (string.IsNullOrEmpty(nekretnina.Slika))
            {
                nekretnina.Slika = existingNekretnina.Slika;
            }
            else
            {
                existingNekretnina.Slika = nekretnina.Slika;
            }

            // Ažuriraj polja Lokacije ako postoji
            if (existingNekretnina.Lokacija != null && nekretnina.Lokacija != null)
            {
                existingNekretnina.Lokacija.grad = nekretnina.Lokacija.grad;
                existingNekretnina.Lokacija.adresa = nekretnina.Lokacija.adresa;
                existingNekretnina.Lokacija.latituda = nekretnina.Lokacija.latituda;
                existingNekretnina.Lokacija.longituda = nekretnina.Lokacija.longituda;
            }

            try
            {
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

            return RedirectToAction("Details", new { id = nekretnina.Id });
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