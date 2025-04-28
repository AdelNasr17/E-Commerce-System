
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


        //Check Email 
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _servicesManager.authenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        //Get Current user 
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var addUser = await _servicesManager.authenticationService.GetCurrentUserAsync(email!);
            return Ok(addUser);
        }
        // Get Current User address 
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _servicesManager.authenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(Address);
        }

        // Update Current User address 
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await _servicesManager.authenticationService.UpdateCurrentUserAddressAsync(email,addressDto);
            return Ok(updatedAddress);
        }

    }
}
