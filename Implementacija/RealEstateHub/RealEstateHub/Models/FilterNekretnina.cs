namespace RealEstateHub.Models{
    public class FilterNekretnina{
        public int filterNekretninaId { get; set; }
        public double minCijena { get; set; }
        public double maxCijena { get; set; }
        public int brojSoba { get; set; }
        public int kvadratura { get; set; }
        public Kategorija tipNekretnine { get; set; }
        public Lokacija lokacija { get; set; }

    }
}
