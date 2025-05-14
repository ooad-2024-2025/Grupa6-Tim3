using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateHub.Models;

namespace RealEstateHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Nekretnina> Nekretnina { get; set; }
        public DbSet<Oglas> Oglas { get; set; }
        public DbSet<Obavjestenje> Obavjestenje { get; set; }
        public DbSet<FilterNekretnina> FilterNekretnina { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Korisnik_Nekretnina> Korisnik_Nekretnina{ get; set; }
        public DbSet<Kriterij> Kriterij { get; set; }
        public DbSet<Lokacija> Lokacija { get; set; }
        public DbSet<Poruka> Poruka { get; set; }
        public DbSet<Sesija> Sesija { get; set; }
        public DbSet<Vlasnik_Nekretnina> Vlasnik_Nekretnina { get; set; }

        //public DbSet<Kategorija> Kategorija { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {      
            modelBuilder.Entity<FilterNekretnina>().ToTable("FilterNekretnina");
            modelBuilder.Entity<Korisnik>().ToTable("Korisnik");
            modelBuilder.Entity<Korisnik_Nekretnina>().ToTable("Korisnik_Nekretnina");
            modelBuilder.Entity<Kriterij>().ToTable("Kriterij");
            modelBuilder.Entity<Lokacija>().ToTable("Lokacija");
            modelBuilder.Entity<Nekretnina>().ToTable("Nekretnina");
            modelBuilder.Entity<Obavjestenje>().ToTable("Obavjestenje");
            modelBuilder.Entity<Oglas>().ToTable("Oglas");
            modelBuilder.Entity<Poruka>().ToTable("Poruka");
            modelBuilder.Entity<Sesija>().ToTable("Sesija");
            modelBuilder.Entity<Vlasnik_Nekretnina>().ToTable("Vlasnik_Nekretnina");
            base.OnModelCreating(modelBuilder);
        }


    }
}
