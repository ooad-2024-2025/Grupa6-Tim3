namespace RealEstateHub.Models
{
    public class PagedNekretnineViewModel
    {
        public IEnumerable<Nekretnina> Nekretnine { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SortOrder { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

}
