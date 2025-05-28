using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Oglas{
        public int oglasId { get; set; }

        [Display(Name = "Aktivan")]
        public bool jeAktivan { get; set; }
        public Nekretnina nekretnina { get; set; }

        [Display(Name = "Datum postavljanja")]
        public DateTime datumPostavljanja { get; set; }

        [Display(Name = "Broj pregleda")]
        public int brojPregleda { get; set; }
    }
}
