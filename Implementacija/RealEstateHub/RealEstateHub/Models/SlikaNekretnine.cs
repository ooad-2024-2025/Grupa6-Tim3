namespace RealEstateHub.Models
{
    public class SlikaNekretnine
    {

        public int Id { get; set; }
        public int NekretninaId { get; set; }
        public string Putanja { get; set; }

        public Nekretnina Nekretnina { get; set; }

    }
}
