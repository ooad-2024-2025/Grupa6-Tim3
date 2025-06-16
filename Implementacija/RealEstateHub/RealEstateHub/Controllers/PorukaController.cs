using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateHub.Data;
using RealEstateHub.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RealEstateHub.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PorukaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PorukaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Poruka

        public async Task<IActionResult> Index()
        {
            return View(await _context.Poruka.ToListAsync());
        }

        // GET: Poruka/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka
                .FirstOrDefaultAsync(m => m.porukaId == id);
            if (poruka == null)
            {
                return NotFound();
            }

            return View(poruka);
        }

        // GET: Poruka/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Poruka/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("porukaId,posiljalacId,primalacId,sadrzaj,procitano,datumSlanja")] Poruka poruka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poruka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poruka);
        }

        // GET: Poruka/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka.FindAsync(id);
            if (poruka == null)
            {
                return NotFound();
            }
            return View(poruka);
        }

        // POST: Poruka/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("porukaId,posiljalacId,primalacId,sadrzaj,procitano,datumSlanja")] Poruka poruka)
        {
            if (id != poruka.porukaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poruka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PorukaExists(poruka.porukaId))
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
            return View(poruka);
        }

        // GET: Poruka/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poruka = await _context.Poruka
                .FirstOrDefaultAsync(m => m.porukaId == id);
            if (poruka == null)
            {
                return NotFound();
            }

            return View(poruka);
        }

        // POST: Poruka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poruka = await _context.Poruka.FindAsync(id);
            if (poruka != null)
            {
                _context.Poruka.Remove(poruka);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MojePoruke));
        }

        private bool PorukaExists(int id)
        {
            return _context.Poruka.Any(e => e.porukaId == id);
        }

        // POST: Poruka/CreateFromNekretnina
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromNekretnina(string primalacId, int nekretninaId, string sadrzaj)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
            {
                return Forbid(); // nije prijavljen korisnik
            }

            if (string.IsNullOrWhiteSpace(sadrzaj))
            {
                TempData["PorukaStatus"] = "Poruka ne može biti prazna.";
                return RedirectToAction("Details", "Nekretnina", new { id = nekretninaId });
            }

            var novaPoruka = new Poruka
            {
                posiljalacId = userIdString,
                primalacId = primalacId,
                sadrzaj = sadrzaj,
                datumSlanja = DateTime.Now,
                procitano = false
            };

            _context.Poruka.Add(novaPoruka);
            await _context.SaveChangesAsync();

            TempData["PorukaStatus"] = "Poruka je uspješno poslana vlasniku.";
            return RedirectToAction("Details", "Nekretnina", new { id = nekretninaId });
        }

        [Authorize]
        public async Task<IActionResult> MojePoruke()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Forbid();

            var razgovori = await _context.Poruka
                .Where(p => p.posiljalacId == userId || p.primalacId == userId)
                .Select(p => p.posiljalacId == userId ? p.primalacId : p.posiljalacId)
                .Distinct()
                .ToListAsync();

            var korisnici = await _context.Users
                .Where(u => razgovori.Contains(u.Id))
                .ToListAsync();

            return View("ListaRazgovora", korisnici);
        }


        // GET: Poruka/Odgovori?primalacId=xxx&porukaId=yyy
        [Authorize]
        public IActionResult Odgovori(string primalacId, int? porukaId)
        {
            if (string.IsNullOrEmpty(primalacId))
                return BadRequest();

            ViewData["PrimalacId"] = primalacId;
            ViewData["PorukaId"] = porukaId;

            return View();
        }

        // POST: Poruka/Odgovori
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Odgovori(string primalacId, string sadrzaj)
        {
            if (string.IsNullOrEmpty(primalacId) || string.IsNullOrEmpty(sadrzaj))
            {
                ModelState.AddModelError("", "Polja ne smiju biti prazna.");
                ViewData["PrimalacId"] = primalacId;
                return View();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var novaPoruka = new Poruka
            {
                posiljalacId = userId,
                primalacId = primalacId,
                sadrzaj = sadrzaj,
                datumSlanja = DateTime.Now,
                procitano = false
            };

            _context.Poruka.Add(novaPoruka);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MojePoruke));
        }

        [Authorize]
        public async Task<IActionResult> Chat(string drugiKorisnikId)
        {
            var trenutniKorisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (trenutniKorisnikId == null || string.IsNullOrEmpty(drugiKorisnikId))
            {
                return BadRequest();
            }

            var poruke = await _context.Poruka
                .Where(p =>
                    (p.posiljalacId == trenutniKorisnikId && p.primalacId == drugiKorisnikId) ||
                    (p.posiljalacId == drugiKorisnikId && p.primalacId == trenutniKorisnikId)
                )
                .Include(p => p.Posiljalac)
                .Include(p => p.Primalac)
                .OrderBy(p => p.datumSlanja)
                .ToListAsync();


            ViewData["DrugiKorisnikId"] = drugiKorisnikId;
            ViewData["TrenutniKorisnikId"] = trenutniKorisnikId;

            return View(poruke);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> PosaljiPoruku(string primalacId, string sadrzaj)
        {
            var posiljalacId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(primalacId) || string.IsNullOrEmpty(sadrzaj) || sadrzaj.Length < 5)
            {
                ModelState.AddModelError("", "Poruka mora imati najmanje 5 znakova.");
                return RedirectToAction("Chat", new { drugiKorisnikId = primalacId });
            }

            var novaPoruka = new Poruka
            {
                posiljalacId = posiljalacId,
                primalacId = primalacId,
                sadrzaj = sadrzaj,
                datumSlanja = DateTime.Now,
                procitano = false
            };

            _context.Poruka.Add(novaPoruka);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { drugiKorisnikId = primalacId });
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ObrisiRazgovor(string drugiKorisnikId)
        {
            var trenutniKorisnikId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (trenutniKorisnikId == null || string.IsNullOrEmpty(drugiKorisnikId))
                return BadRequest();

            var poruke = await _context.Poruka
                .Where(p =>
                    (p.posiljalacId == trenutniKorisnikId && p.primalacId == drugiKorisnikId) ||
                    (p.posiljalacId == drugiKorisnikId && p.primalacId == trenutniKorisnikId))
                .ToListAsync();

            _context.Poruka.RemoveRange(poruke);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MojePoruke));
        }

    }
}