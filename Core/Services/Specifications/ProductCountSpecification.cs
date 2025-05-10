using Shared.Product;

namespace Services.Specifications
{
    public class ProductCountSpecification:Specifications<Product>
    {
        public ProductCountSpecification(ProductSpecificationsParameters parameters)
        : base(product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
            (!parameters.typeId.HasValue || product.TypeId == parameters.typeId.Value)
        && (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
        {
           
        }
    }
}
