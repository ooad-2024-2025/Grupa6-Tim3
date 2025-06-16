using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealEstateHub.Models{
    public class Lokacija{
        public int lokacijaId { get; set; }
        public int nekretninaId { get; set; }

        [Display(Name = "Grad")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage =
            "Naziv grada mora imati između 3 i 50 znakova!")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Dozvoljeno je samo korištenje velikih i malih slova i razmaka!")]
        public string grad { get; set; }

        [Display(Name = "Adresa")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage =
            "Adresa nekretnine mora imati između 5 i 50 znakova!")]
        public string adresa { get; set; }

        [Display(Name = "Latituda")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Dozvoljeni su samo brojevi i tačka!")]
        public double latituda { get; set; }

        [Display(Name = "Longituda")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Dozvoljeni su samo brojevi i tačka!")]
        public double longituda { get; set; }

        [JsonIgnore]
        public Nekretnina? Nekretnina { get; set; }
        internal char ToLower()
        {
            throw new NotImplementedException();
        }
    }
}
