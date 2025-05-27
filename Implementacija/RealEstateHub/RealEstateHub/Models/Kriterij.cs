using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Kriterij{
        public int kriterijId { get; set; }
        public int obavjestenjeId { get; set; }

        [Display(Name = "Vrijednost")]
        public string vrijednost { get; set; }

    }
}
