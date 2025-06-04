using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public enum Kategorija {
        [Display(Name="Kuća")]
        KUCA,
        [Display(Name = "Stan")]
        STAN,
        [Display(Name = "Poslovni prostor")]
        POSLOVNI_PROSTOR,
        [Display(Name = "Garaža")]
        GARAZA,
        [Display(Name = "Vikendica")]
        VIKENDICA,
        [Display(Name = "Zemljište")]
        ZEMLJISTE
    }
}
