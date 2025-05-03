

using Domain.Entities.Identity;
using Shared.Identity.Dto;

namespace Services.Mapping_Profiles
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
