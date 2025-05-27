using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Nekretnina{
        public int Id { get; set; }

        [Display(Name = "Naslov")]
        public string naslov { get; set; }

        [Display(Name = "Opis nekretnine")]
        public string opisNekretnine { get; set; }

        [Display(Name = "Cijena")]
        public double cijena { get; set; }

        [Display(Name = "Kvadratura")]
        public double kvadratura { get; set; }

        [Display(Name = "Lokacija")]
        public string lokacija { get; set; }

        [Display(Name = "Broj soba")]
        public int brojSoba { get; set; }

        public int vlasnikId { get; set; }

        [Display(Name = "Vrsta nekretnine")]
        [EnumDataType(typeof(Kategorija))]
        public Kategorija vrstaNekretnine { get; set; }
    }
}
