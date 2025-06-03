using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateHub.Models
{
    public class Poruka
    {
        [Key]
        public int porukaId { get; set; }

        public string posiljalacId { get; set; }
        [ForeignKey("posiljalacId")]
        public ApplicationUser Posiljalac { get; set; }

        public string primalacId { get; set; }
        [ForeignKey("primalacId")]
        public ApplicationUser Primalac { get; set; }

        [Display(Name = "Sadrzaj")]
        [StringLength(maximumLength: 200, MinimumLength = 5,
            ErrorMessage = "Poruka mora imati između 5 i 200 znakova!")]
        public string sadrzaj { get; set; }

        [Display(Name = "Procitano")]
        public bool procitano { get; set; }

        [Display(Name = "Datum slanja")]
        public DateTime datumSlanja { get; set; }
    }
}
