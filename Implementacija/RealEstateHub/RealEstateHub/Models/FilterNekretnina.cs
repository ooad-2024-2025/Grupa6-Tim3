using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models
{
    public class FilterNekretnina
    {
        public int filterNekretninaId { get; set; }

        [Display(Name = "Minimalna cijena")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Dozvoljen je unos samo brojeva i decimalne tačke")]
        public double minCijena { get; set; }

        [Display(Name = "Maksimalna cijena")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Dozvoljen je unos samo brojeva i decimalne tačke")]
        public double maxCijena { get; set; }

        [Display(Name = "Minimalan broj soba")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Dozvoljen je unos samo brojeva")]
        public int minBrojSoba { get; set; }

        [Display(Name = "Maksimalan broj soba")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Dozvoljen je unos samo brojeva")]
        public int maxBrojSoba { get; set; }

        [Display(Name = "Minimalna kvadratura")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Dozvoljen je unos samo brojeva i decimalne tačke")]
        public int minKvadratura { get; set; }

        [Display(Name = "Maksimalna kvadratura")]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage = "Dozvoljen je unos samo brojeva i decimalne tačke")]
        public int maxKvadratura { get; set; }

        [Display(Name = "Tip nekretnine")]
        [EnumDataType(typeof(Kategorija))]
        public Kategorija tipNekretnine { get; set; }

         //[Display(Name = "Lokacija")]
        //public Lokacija lokacija { get; set; }

        public string KorisnikId { get; set; }

        //dodano
        [Display(Name = "Primi obavještenja o novim nekretninama")]
        public bool ZeliObavjestenja { get; set; }

        //dodano
    }
}
