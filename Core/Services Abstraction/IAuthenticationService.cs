

using Shared.Identity.Dto;

namespace Services_Abstraction
{
    public interface IAuthenticationService
    {
        //Login 
        //Take Email and Password Then Return Token ,  Email and DisplayName To Client  

        public Task<UserDto> LoginAsync(LoginDto loginDto);



        //Register
        // Take Email , Password  , UserName , Display Name And Phone Number
        // Then Return Token , Email and Display Name To Client 

        public Task<UserDto> RegisterAsync(RegisterDto registerDto);

        //Check Email
        //Take Email Then Return boolean To Client
        public Task<bool> CheckEmailAsync(string email);



        //Get Current User Address
        //Take Email Then Return Address of Current Logged in User To Client  


        public Task<AddressDto> GetCurrentUserAddressAsync(string email);

        //Update Current User Address
        //Take AdressDto Updated Address and Email Then Return AddressDto after Update To Client  

        public Task<AddressDto> UpdateCurrentUserAddressAsync(string email,AddressDto addressDto);
        //Get Current User
        //Take Email Then Return UserDto Token , Email and Display Name To Client  

        public Task<UserDto> GetCurrentUserAsync(string email);
    }
}
