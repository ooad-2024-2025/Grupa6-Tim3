using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class FilterNekretnina{
        public int filterNekretninaId { get; set; }

        [Display(Name = "Minimalna cijena")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Dozvoljen je unos samo brojeva i decimalne tačke")]
        public double minCijena { get; set; }

        [Display(Name = "Maksimalna cijena")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Dozvoljen je unos samo brojeva i decimalne tačke")]
        public double maxCijena { get; set; }

        [Display(Name = "Broj soba")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Dozvoljen je unos samo brojeva")]
        public int brojSoba { get; set; }

        [Display(Name = "Kvadratura")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Dozvoljen je unos samo brojeva i decimalne tačke")]
        public int kvadratura { get; set; }

        [Display(Name = "Tip nekretnine")]
        [EnumDataType(typeof(Kategorija))]
        public Kategorija tipNekretnine { get; set; }

        [Display(Name = "Lokacija")]
        public Lokacija lokacija { get; set; }

    }
}
