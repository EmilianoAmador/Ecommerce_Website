using Core.Entities;

namespace Core.Specification
{

    // This specification class helps us get the count of items in a page so we can populate Specifications in our Pagination class
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) : base(x =>
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&  // if Id present (then left is false and right of the or/else condition excecutes) && allows to pass both filters 
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
        }
    }
}