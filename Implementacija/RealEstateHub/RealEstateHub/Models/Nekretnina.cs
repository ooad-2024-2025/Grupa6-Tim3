using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateHub.Models{
    public class Nekretnina{
        public int Id { get; set; }

        [Display(Name = "Naslov")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage =
            "Naziv nekretnine smije imati između 5 i 50 znakova!")]
        [RegularExpression(@"^[0-9a-zA-Z ,\-]*$", ErrorMessage =
            "Dozvoljena su samo slova, brojevi, razmaci, zarezi i crte!")]
        public string naslov { get; set; }

        [Display(Name = "Opis nekretnine")]
        [StringLength(maximumLength: 1000, MinimumLength = 5, ErrorMessage =
            "Opis nekretnine mora imati manje od 1000 znakova!")]
        public string opisNekretnine { get; set; }

        [Display(Name = "Cijena")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Dozvoljeni su samo brojevi i tačka!")]
        public double cijena { get; set; }

        [Display(Name = "Kvadratura")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Dozvoljeni su samo brojevi i tačka!")]
        public double kvadratura { get; set; }

        /*
        [Display(Name = "Lokacija")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage =
            "Lokacija nekretnine smije imati između 5 i 50 znakova!")]
        public string lokacija { get; set; }*/

        [Display(Name = "Broj soba")]
        [Range(0, int.MaxValue, ErrorMessage = "Dozvoljeni su samo brojevi!")]
        public int brojSoba { get; set; }

        [Display(Name = "Vrsta nekretnine")]
        [EnumDataType(typeof(Kategorija))]
        public Kategorija vrstaNekretnine { get; set; }

        public string VlasnikId { get; set; }

        [ForeignKey("VlasnikId")]
        public ApplicationUser Vlasnik { get; set; }

        //public string Slika { get; set; }

        public List<SlikaNekretnine> Slike { get; set; } = new List<SlikaNekretnine>();

        // Ovo nije mapirano u bazu, služi samo za primanje Base64 stringova iz forme
        [NotMapped]
        public IFormFile[] UploadaneSlike { get; set; }



        public Lokacija Lokacija { get; set; } = new Lokacija();

        //dodano
        public int BrojPregleda { get; set; } = 0;


        public DateTime DatumDodavanja { get; set; } = DateTime.Now;
    }
}
