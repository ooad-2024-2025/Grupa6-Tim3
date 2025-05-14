namespace RealEstateHub.Models
{
    public class Poruka
    {

        public int porukaId { get; set; }
        public int posiljalacId { get; set; }
        public int primalacId { get; set; }

        public string sadrzaj { get; set; }
        public bool procitano { get; set; }
        public DateTime datumSlanja { get; set; }
    }
}
