using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models
{
    public class Vlasnik_Nekretnina
    {
        [Key]
        public int vn_id { get; set; }

        public int nekretninaId { get; set; }

        public int vlasnikId { get; set; }
    }
}
