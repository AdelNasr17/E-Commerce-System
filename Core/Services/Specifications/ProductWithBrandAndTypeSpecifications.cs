using Shared.Product;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        // Constructor to retrieve a product by its ID, including its Brand and Type
        public ProductWithBrandAndTypeSpecifications(int id) : base(product => product.Id == id)
        {
            // Include related entities for eager loading
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }

        // Constructor to retrieve products based on brand and type filters, with optional sorting
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameters parameters)
            : base(product =>
                (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
                (!parameters.typeId.HasValue || product.TypeId == parameters.typeId.Value)&&
          (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim()))
            )

        {
            // Include related entities for eager loading
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);


            // Handle sorting based on the provided sort parameter
            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescending(p => p.Price);
                        break;
                    case ProductSortOptions.priceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }

            ApplyPagination(parameters.PageIndex, parameters.PageSize);
        }
    }
}
