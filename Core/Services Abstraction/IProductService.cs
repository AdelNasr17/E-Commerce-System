using Shared;
using Shared.Product;
using Shared.Product.Dto;

namespace Services_Abstraction
{
    public interface IProductService
    {
        //Get all Product
        public Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters);
        //Get All Brands
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //Get All Types
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        // Get Product By Id
        public Task<ProductResultDto> GetProductByIdAsync(int id);


    }
}
