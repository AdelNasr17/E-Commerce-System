

using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.Identity.Dto;

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
    }
}
