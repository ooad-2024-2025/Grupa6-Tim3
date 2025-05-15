namespace RealEstateHub.Models{
    public class Korisnik{
        public int korisnikId { get; set; }
        public string imeKorisnika { get; set; }
        public string prezimeKorisnika { get; set; }
        public string emailKorisnika { get; set; }
        public string lozinkaKorisnika { get; set; }
        public string brojTelefona { get; set; }
        public DateTime datumPridruzivanja { get; set; }
    }
}
