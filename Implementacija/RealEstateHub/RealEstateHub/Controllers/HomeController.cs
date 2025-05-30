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

        // Ažuriraj Index akciju da dohvati tri najnovije nekretnine
        public async Task<IActionResult> Index()
        {
            // Dohvati tri najnovije nekretnine po ID-u (pretpostavljamo da je veæi ID noviji unos)
            var najnovijeNekretnine = await _context.Nekretnina
                                                .OrderByDescending(n => n.Id)
                                                .Take(3)
                                                .ToListAsync();
            return View(najnovijeNekretnine); 
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