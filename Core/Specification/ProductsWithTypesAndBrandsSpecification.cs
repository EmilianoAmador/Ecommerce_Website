using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    /// This Class enables feature functionality in our front end
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&     // if search bar is empty
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&                      // if Id present (then left is false and right of the (|| - else) condition excecutes) && allows to pass both filters 
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            
            // Allows sorting by name
            AddOrderBy(x => x.Name);

            // Allows for paging to exclude the products from previous pages so that wach page has their own products listed w/o repetition
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            // Checks the value of the sort in order to allow users to sort name alphabetically multiple ways
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;

                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}