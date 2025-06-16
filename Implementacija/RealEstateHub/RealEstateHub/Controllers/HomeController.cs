using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RealEstateHub.Models;
using RealEstateHub.Data;   
using System.Linq;          
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore; 

namespace RealEstateHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; 

        // Ažuriraj konstruktor da prima ApplicationDbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public async Task<IActionResult> Index(string? searchTerm)
        {
            // Tri najnovije nekretnine (bez filtera)
            var najnovije = await _context.Nekretnina
                .Include(n => n.Slike)
                .OrderByDescending(n => n.Id)
                .Take(3)
                .ToListAsync();

            // Filtrirane nekretnine za mapu - samo ako postoji searchTerm
            IQueryable<Nekretnina> mapaQuery = _context.Nekretnina
                .Include(n => n.Lokacija)
                .Include(n => n.Slike)
                .Where(n => n.Lokacija != null && n.Lokacija.latituda != 0 && n.Lokacija.longituda != 0);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                mapaQuery = mapaQuery.Where(n => n.naslov.Contains(searchTerm));
            }

            var filtriraneZaMapu = await mapaQuery.ToListAsync();

            ViewBag.SveSaLokacijom = filtriraneZaMapu;
            return View(najnovije);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}