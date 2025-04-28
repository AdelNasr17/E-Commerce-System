

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.Identity.Dto;
using System.Security.Claims;

namespace Presentation
{
    
    public class AuthenticationController(IServicesManager _servicesManager) : ApiBaseController
    {
        //Login 
        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var User= await _servicesManager.authenticationService.LoginAsync(loginDto);

            return Ok(User);


        }

        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto  registerDto)
        {
            var User = await _servicesManager.authenticationService.RegisterAsync(registerDto);

            return Ok(User);


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
            var updatedAddress = await _servicesManager.authenticationService.UpdateCurrentUserAddressAsync(email, addressDto);
            return Ok(updatedAddress);
        }
    }
}
