namespace RealEstateHub.Models{
    public class Nekretnina{
        public int Id { get; set; }

        public string naslov { get; set; }

        public string opisNekretnine { get; set; }

        public double cijena { get; set; }

        public double kvadratura { get; set; }

        public string lokacija { get; set; }

        public int brojSoba { get; set; }

        public int vlasnikId { get; set; }  
        
        public Kategorija vrstaNekretnine { get; set; }
    }
}
