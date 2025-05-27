using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Obavjestenje{
        public int obavjestenjeId { get; set; }
        public int korisnikId { get; set; }

        [Display(Name = "Datum obavjestenja")]
        public DateTime datumObavjestenja { get; set; }

        [Display(Name = "Poruka")]
        public string poruka { get; set; }

        [Display(Name = "Kriterij")]
        public Kriterij kriterij { get; set; }
    }
}
