using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Obavjestenje{
        public int obavjestenjeId { get; set; }
        public int korisnikId { get; set; }

        [Display(Name = "Datum obavjestenja")]
        public DateTime datumObavjestenja { get; set; }

        [Display(Name = "Poruka")]
        [StringLength(maximumLength: 200, MinimumLength = 5, ErrorMessage =
            "Poruka mora imati između 5 i 200 znakova!")]
        public string poruka { get; set; }

        [Display(Name = "Kriterij")]
        public Kriterij kriterij { get; set; }

        public ApplicationUser Korisnik { get; set; }
    }
}
