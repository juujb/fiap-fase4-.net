namespace FIAP.IRRIGACAO.API.ViewModel
{
    public class PagedViewModel<T>
    {
        public required IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => Data.Count() == PageSize;
    }
}
