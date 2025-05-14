namespace RealEstateHub.Models

{
    public class Oglas
    {

        public int oglasId { get; set; }
        public bool jeAktivan { get; set; }
        public Nekretnina nekretnina { get; set; }
        public DateTime datumPostavljanja { get; set; }
        public int brojPregleda { get; set; }
    }
}
