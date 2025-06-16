using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RealEstateHub.Data;
using RealEstateHub.Models;
using RealEstateHub.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateHub.Services
{
    public class ObavjestenjeService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _baseUrl;

        public ObavjestenjeService(IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _baseUrl = configuration.GetValue<string>("AppSettings:BaseUrl");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var noviOglasi = await db.Nekretnina
                    .Where(n => n.DatumDodavanja >= DateTime.Now.AddMinutes(-2))
                    .ToListAsync();


                var filteri = await db.FilterNekretnina
                    .Where(f => f.ZeliObavjestenja)
                    .ToListAsync();

                foreach (var filter in filteri)
                {
                    var odgovarajuce = noviOglasi.Where(n =>
                        n.cijena >= filter.minCijena &&
                        n.cijena <= filter.maxCijena &&
                        n.brojSoba >= filter.minBrojSoba &&
                        n.brojSoba <= filter.maxBrojSoba &&
                        n.kvadratura >= filter.minKvadratura &&
                        n.kvadratura <= filter.maxKvadratura &&
                        n.vrstaNekretnine == filter.tipNekretnine
                    ).ToList();

                    foreach (var nekretnina in odgovarajuce)
                    {
                        // Provjera da korisnik nije vlasnik nekretnine
                        if (nekretnina.VlasnikId != filter.KorisnikId)
                        {
                            bool vecPoslano = await db.PoslanaObavjestenja
                                .AnyAsync(p => p.NekretninaId == nekretnina.Id && p.KorisnikId == filter.KorisnikId);

                            if (!vecPoslano)
                            {
                                var korisnik = await db.Users.FindAsync(filter.KorisnikId);                                

                                string body = $"Poštovani, pronađena je nova nekretnina koja odgovara vašim kriterijima: <br/>" +
                                              $"<a href='{_baseUrl.TrimEnd('/')}/Nekretnina/Details/{nekretnina.Id}'>Pogledaj oglas</a>";


                                await EmailHelper.SendEmailAsync(korisnik.Email, "Nova nekretnina po vašim kriterijima", body);

                                db.PoslanaObavjestenja.Add(new PoslanoObavjestenje
                                {
                                    NekretninaId = nekretnina.Id,
                                    KorisnikId = filter.KorisnikId,
                                    DatumSlanja = DateTime.Now
                                });

                                await db.SaveChangesAsync();
                            }
                        }
                    }

                }


                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);

            }
        }
    }
}
