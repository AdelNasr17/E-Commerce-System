
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared;
using Shared.Identity.Dto;
using Shared.Product;
using Shared.Product.Dto;
using System.Security.Claims;

namespace Presentation
{
   
    public class ProductController(IServicesManager _servicesManager): ApiBaseController
    {
        //Get
        [Authorize]
        [HttpGet]
     
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>>GetAllProducts([FromQuery] ProductSpecificationsParameters parameters)
        {
            //parameters ==> Sort,BrandId , TypeId , PageSize,PageIndex
            var Products = await _servicesManager.ProductService.GetAllProductsAsync(parameters);
            return Ok(Products);
        }


        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var Brands = await _servicesManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var Types = await _servicesManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProduct(int id)
        {
            var product = await _servicesManager.ProductService.GetProductByIdAsync(id);
          
            return Ok(product);
        }


     

    }
}
