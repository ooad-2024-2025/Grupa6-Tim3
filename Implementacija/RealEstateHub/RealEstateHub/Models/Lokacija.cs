using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Lokacija{
        public int lokacijaId { get; set; }

        public int nekretninaId { get; set; }

        [Display(Name = "Grad")]

        public string grad { get; set; }

        [Display(Name = "Adresa")]

        public string adresa { get; set; }

        [Display(Name = "Latituda")]

        public double latituda { get; set; }

        [Display(Name = "Longituda")]

        public double longituda { get; set; }
    }
}
