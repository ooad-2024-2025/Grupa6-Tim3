using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateHub.Models
{
    public class PoslanoObavjestenje
    {
        public int Id { get; set; }

        [Required]
        public int NekretninaId { get; set; }

        [ForeignKey("NekretninaId")]
        public Nekretnina Nekretnina { get; set; }

        [Required]
        public string KorisnikId { get; set; }

        [ForeignKey("KorisnikId")]
        public ApplicationUser Korisnik { get; set; }

        public DateTime DatumSlanja { get; set; }
    }
}
