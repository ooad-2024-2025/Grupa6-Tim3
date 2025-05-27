using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Korisnik{
        public int korisnikId { get; set; }

        [Display(Name = "Ime")]
        public string imeKorisnika { get; set; }

        [Display(Name = "Prezime")]
        public string prezimeKorisnika { get; set; }

        [Display(Name = "Email")]
        public string emailKorisnika { get; set; }

        [Display(Name = "Lozinka")]
        public string lozinkaKorisnika { get; set; }

        [Display(Name = "Broj telefona")]
        public string brojTelefona { get; set; }

        [Display(Name = "Datum pridruzivanja")]
        public DateTime datumPridruzivanja { get; set; }
    }
}
