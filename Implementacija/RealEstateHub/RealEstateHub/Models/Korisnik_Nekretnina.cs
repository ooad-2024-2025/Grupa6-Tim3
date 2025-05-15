using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Korisnik_Nekretnina{
        [Key]
        public int kn_id { get; set; }

        public int nekretninaId { get; set; }

        public int korisnikId { get; set;}
    }
}
