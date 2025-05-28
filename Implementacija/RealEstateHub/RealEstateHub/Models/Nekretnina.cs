using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Nekretnina{
        public int Id { get; set; }

        [Display(Name = "Naslov")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage =
            "Naziv nekretnine smije imati između 5 i 50 znakova!")]
        [RegularExpression(@"[0-9| |a-z|A-Z]*", ErrorMessage =
            "Dozvoljeno je samo korištenje velikih i malih slova, brojeva i razmaka!")]
        public string naslov { get; set; }

        [Display(Name = "Opis nekretnine")]
        [StringLength(maximumLength: 1000, MinimumLength = 5, ErrorMessage =
            "Opis nekretnine mora imati manje od 1000 znakova!")]
        public string opisNekretnine { get; set; }

        [Display(Name = "Cijena")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Dozvoljeni su samo brojevi i tačka!")]
        public double cijena { get; set; }

        [Display(Name = "Kvadratura")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Dozvoljeni su samo brojevi i tačka!")]
        public double kvadratura { get; set; }

        [Display(Name = "Lokacija")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage =
            "Lokacija nekretnine smije imati između 5 i 50 znakova!")]
        public string lokacija { get; set; }

        [Display(Name = "Broj soba")]
        [Range(1, int.MaxValue, ErrorMessage = "Dozvoljeni su samo brojevi!")]
        public int brojSoba { get; set; }

        public int vlasnikId { get; set; }

        [Display(Name = "Vrsta nekretnine")]
        [EnumDataType(typeof(Kategorija))]
        public Kategorija vrstaNekretnine { get; set; }
    }
}
