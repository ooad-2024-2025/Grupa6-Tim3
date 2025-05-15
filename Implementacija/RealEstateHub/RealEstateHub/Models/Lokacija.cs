namespace RealEstateHub.Models{
    public class Lokacija{
        public int lokacijaId { get; set; }

        public int nekretninaId { get; set; }
    
        public string grad { get; set; }

        public string adresa { get; set; }

        public double latituda { get; set; }

        public double longituda { get; set; }
    }
}
