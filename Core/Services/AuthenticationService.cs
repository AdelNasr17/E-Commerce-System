using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Identity.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return false;
            else
                return true;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
           var user= await _userManager.Users.Include(u=> u.Address).FirstOrDefaultAsync(u=> u.Email == email) ?? throw new UserNotFoundException(email);

            if(user.Address is not null)
          return _mapper.Map<AddressDto>(user.Address);
            else
                throw new AddressNotFoundException(user.UserName);
             
          


        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UserNotFoundException(email);
            else
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user),
                };
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
            if(user.Address is not null)//Update
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Country=addressDto.Country;
                user.Address.Street = addressDto.Street;
                user.Address.City= addressDto.City;

            }
            else //addNew adress
            {
                user.Address=_mapper.Map<AddressDto,Address>(addressDto);
            }

          await   _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            //Check if Email Is Exsits
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                throw new UserNotFoundException(loginDto.Email);


            //Check if Password

            var IsPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (IsPassword)
                //Return UserDto 
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)

                };


            else
                throw new UnauthorizedException();

        }

    

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Mapping Register Dto => ApplicationUser
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };
            
           //Create User [ApplicationUser]
           var Result = await _userManager.CreateAsync(user,registerDto.Password);
            if(Result.Succeeded)
            {
                //Return User Dto 
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                var  Errors= Result.Errors.Select(e=> e.Description).ToList();
                throw new BadRequestException(Errors);

            }



        }

        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
          {
              new (ClaimTypes.Email,user.Email!),
              new (ClaimTypes.Name,user.UserName!),
              new (ClaimTypes.NameIdentifier,user.Id!),

          };
            var Roles= await _userManager.GetRolesAsync(user);

            foreach (var Role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, Role));

            var secretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var Creds= new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );


            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
