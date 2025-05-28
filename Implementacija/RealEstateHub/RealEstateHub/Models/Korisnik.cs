using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Korisnik{
        public int korisnikId { get; set; }

        [Display(Name = "Ime")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova i razmaka!")]
        public string imeKorisnika { get; set; }

        [Display(Name = "Prezime")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova i razmaka!")]
        public string prezimeKorisnika { get; set; }

        [Display(Name = "Email")]
        public string emailKorisnika { get; set; }

        [Display(Name = "Lozinka")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage =
            "Lozinka mora imati između 5 i 50 znakova!")]
        public string lozinkaKorisnika { get; set; }

        [Display(Name = "Broj telefona")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Dozvoljen je unos samo brojeva")]
        public string brojTelefona { get; set; }

        [Display(Name = "Datum pridruzivanja")]
        public DateTime datumPridruzivanja { get; set; }
    }
}
