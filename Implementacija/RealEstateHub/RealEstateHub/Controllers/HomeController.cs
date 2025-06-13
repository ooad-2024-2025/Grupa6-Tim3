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

        // Ažuriraj Index akciju da dohvati tri najnovije nekretnine i prikaze mapu
        public async Task<IActionResult> Index()
        {
            var najnovije = await _context.Nekretnina
                .OrderByDescending(n => n.Id)
                .Take(3)
                .ToListAsync();

            var saLokacijom = await _context.Nekretnina
                .Include(n => n.Lokacija)
                .Where(n => n.Lokacija != null && n.Lokacija.latituda != 0 && n.Lokacija.longituda != 0)
                .ToListAsync();

            ViewBag.SveSaLokacijom = saLokacijom;

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