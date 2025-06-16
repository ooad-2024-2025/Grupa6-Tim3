using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateHub.Data;
using RealEstateHub.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateHub.Controllers
{
    public class NekretninaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NekretninaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, int page = 1, int pageSize = 6)
        {
            ViewData["CijenaSort"] = String.IsNullOrEmpty(sortOrder) ? "cijena_desc" : "";
            ViewData["KvadraturaSort"] = sortOrder == "kvadratura" ? "kvadratura_desc" : "kvadratura";
            ViewData["SobeSort"] = sortOrder == "sobe" ? "sobe_desc" : "sobe";

            var nekretnine = _context.Nekretnina
                .Include(n => n.Slike) // Učitaj slike da budu dostupne u view-u
                .AsQueryable();

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
                .Include(n => n.Slike)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nekretnina == null)
                return NotFound();

            var currentUserId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
            bool isOwner = (currentUserId != null) && (nekretnina.VlasnikId == currentUserId);

            if (!isOwner)
            {
                nekretnina.BrojPregleda++;
                await _context.SaveChangesAsync();
            }

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
        public async Task<IActionResult> Create(Nekretnina nekretnina)
        {
            ModelState.Remove("Vlasnik");
            ModelState.Remove("VlasnikId");

            if (!ModelState.IsValid)
                return View(nekretnina);

            nekretnina.VlasnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            nekretnina.Slike = new List<SlikaNekretnine>();

            if (nekretnina.UploadaneSlike != null && nekretnina.UploadaneSlike.Length > 0)
            {
                foreach (var fajl in nekretnina.UploadaneSlike)
                {
                    if (fajl != null && fajl.Length > 0)
                    {
                        var imeFajla = Guid.NewGuid().ToString() + Path.GetExtension(fajl.FileName);
                        var putanjaFoldera = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "nekretnine");

                        if (!Directory.Exists(putanjaFoldera))
                            Directory.CreateDirectory(putanjaFoldera);

                        var punaPutanja = Path.Combine(putanjaFoldera, imeFajla);

                        using (var stream = new FileStream(punaPutanja, FileMode.Create))
                        {
                            await fajl.CopyToAsync(stream);
                        }

                        nekretnina.Slike.Add(new SlikaNekretnine
                        {
                            Putanja = "/uploads/nekretnine/" + imeFajla
                        });
                    }
                }
            }

            _context.Add(nekretnina);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var nekretnina = await _context.Nekretnina
                .Include(n => n.Lokacija)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (nekretnina == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (nekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
                return Forbid();

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
                return NotFound();

            var existingNekretnina = await _context.Nekretnina
                .Include(n => n.Lokacija)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (existingNekretnina == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (existingNekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
                return Forbid();

            existingNekretnina.naslov = nekretnina.naslov;
            existingNekretnina.opisNekretnine = nekretnina.opisNekretnine;
            existingNekretnina.cijena = nekretnina.cijena;
            existingNekretnina.kvadratura = nekretnina.kvadratura;
            existingNekretnina.brojSoba = nekretnina.brojSoba;
            existingNekretnina.vrstaNekretnine = nekretnina.vrstaNekretnine;

            if (nekretnina.Slike == null || !nekretnina.Slike.Any())
            {
                nekretnina.Slike = existingNekretnina.Slike;
            }
            else
            {
                existingNekretnina.Slike = nekretnina.Slike;
            }

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
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction("Details", new { id = nekretnina.Id });
        }


        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var nekretnina = await _context.Nekretnina
                .Include(n => n.Slike)         // učitava slike zajedno sa nekretninom
                .Include(n => n.Lokacija)      // opcionalno, ako koristiš lokaciju u Delete view-u
                .FirstOrDefaultAsync(m => m.Id == id);


            if (nekretnina == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (nekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
                return Forbid();

            return View(nekretnina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nekretnina = await _context.Nekretnina.FindAsync(id);

            if (nekretnina == null)
                return NotFound();

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (nekretnina.VlasnikId != currentUserId && !User.IsInRole("Administrator"))
                return Forbid();

            // Prvo izbriši sva PoslanaObavjestenja povezana sa ovom nekretninom
            var poslanaObavjestenja = _context.PoslanaObavjestenja.Where(p => p.NekretninaId == id);
            _context.PoslanaObavjestenja.RemoveRange(poslanaObavjestenja);

            // Sačuvaj promjene nakon brisanja obavještenja
            await _context.SaveChangesAsync();

            // Sada možeš bez problema obrisati nekretninu
            _context.Nekretnina.Remove(nekretnina);
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
            var query = _context.Nekretnina
                .Include(n => n.Slike)
                .AsQueryable();

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

            // 🟢 Ako korisnik želi obavještenja, sačuvaj filter
            if (filter.ZeliObavjestenja && User.Identity.IsAuthenticated)
            {
                var korisnikId = _userManager.GetUserId(User); // preuzima Id trenutno prijavljenog korisnika
                filter.KorisnikId = korisnikId;

                _context.FilterNekretnina.Add(filter);
                await _context.SaveChangesAsync();
            }

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
