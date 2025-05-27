using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public class Poruka{
        public int porukaId { get; set; }
        public int posiljalacId { get; set; }
        public int primalacId { get; set; }

        [Display(Name = "Sadrzaj")]
        public string sadrzaj { get; set; }

        [Display(Name = "Procitano")]
        public bool procitano { get; set; }

        [Display(Name = "Datum slanja")]
        public DateTime datumSlanja { get; set; }
    }
}
