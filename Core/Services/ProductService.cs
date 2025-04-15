
global using Services_Abstraction;
global using Shared.Product.Dto;
global using Domain.Contracts;
global using AutoMapper;
global using Domain.Entities.product;
using Services.Specifications;
using Shared;
using Shared.Product;
using Domain.Exceptions;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            //1. Retrive for All Brands==> UnitOfWork
            //2.Mapping To BrandResultDto==> IMapper
            //3.Return

            var brands =await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
          var brandResult=  _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandResult;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameters parameters)
        {
            //1. Retrive for All products==> UnitOfWork
            //2.Mapping To productResultDto==> IMapper
            //3.Return
            var products = await  _unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var TotalCount = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecification(parameters));

            var ProductsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);


            var Result = new PaginatedResult<ProductResultDto>(
              ProductsResult.Count(),
                parameters.PageIndex,
                 TotalCount,
                ProductsResult
                );

            return Result;

        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var typesResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);

            return typesResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
           var product=await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            //var productResult = _mapper.Map<ProductResultDto>(product);
            //return productResult;

            return product is not null ?throw new ProductNotFoundException(id) : _mapper.Map<ProductResultDto>(product);
        }
    }
}
