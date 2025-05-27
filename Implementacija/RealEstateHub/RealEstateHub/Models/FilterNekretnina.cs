using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class FilterNekretnina{
        public int filterNekretninaId { get; set; }

        [Display(Name = "Minimalna cijena")]
        public double minCijena { get; set; }

        [Display(Name = "Maksimalna cijena")]
        public double maxCijena { get; set; }

        [Display(Name = "Broj soba")]
        public int brojSoba { get; set; }

        [Display(Name = "Kvadratura")]
        public int kvadratura { get; set; }

        [Display(Name = "Tip nekretnine")]
        [EnumDataType(typeof(Kategorija))]
        public Kategorija tipNekretnine { get; set; }

        [Display(Name = "Lokacija")]
        public Lokacija lokacija { get; set; }

    }
}
