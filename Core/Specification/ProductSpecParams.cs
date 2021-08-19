namespace Core.Specification
{

    // This class extends creates the parameters for the http string which is used in browser to make calls to our API defined in ProductsController.cs 
    public class ProductSpecParams
    {
        // Pagination parameter extensions
        private const int MaxPageSize = 50;
        public int PageIndex {get; set;} = 1;

            // page size private and public. Public is a full property bc get and set must be specific. Set restricts user from getting more than 50 request per page
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        // Sets the parameters in our HTTP that fetches API data 
        public int? BrandId {get; set;}
        public int? TypeId {get; set;}
        public string Sort { get; set;}
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

    }
}