using System.ComponentModel.DataAnnotations;

namespace RealEstateHub.Models{
    public enum Kategorija {
        [Display(Name="Kuca")]
        KUCA,
        [Display(Name = "Stan")]
        STAN,
        [Display(Name = "Poslovni prostor")]
        POSLOVNI_PROSTOR,
        [Display(Name = "Garaza")]
        GARAZA,
        [Display(Name = "Vikendica")]
        VIKENDICA,
        [Display(Name = "Zemljiste")]
        ZEMLJISTE
    }
}
