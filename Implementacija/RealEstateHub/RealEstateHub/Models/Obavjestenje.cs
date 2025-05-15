namespace RealEstateHub.Models{
    public class Obavjestenje{
        public int obavjestenjeId { get; set; }
        public int korisnikId { get; set; }
        public DateTime datumObavjestenja { get; set; }
        public string poruka { get; set; }
        public Kriterij kriterij { get; set; }
    }
}
