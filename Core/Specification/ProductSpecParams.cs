namespace Core.Specification
{

    // This class extends the parameters for the ProductController.cs
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

        // BrandId and TypeId Parameters
        public int? BrandId {get; set;}
        public int? TypeId {get; set;}

        // Sorting Parameters
        public string Sort { get; set;}

    }
}