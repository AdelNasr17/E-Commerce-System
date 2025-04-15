using Domain.Entities.product;
using Shared.Product.Dto;


namespace Services.Mapping_Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();

            CreateMap<Product, ProductResultDto>()
                .ForMember(d => d.BrandName, Options => Options.MapFrom(S => S.ProductBrand.Name))
                .ForMember(d => d.TypeName, Options => Options.MapFrom(S => S.ProductType.Name))
                .ForMember(d=>d.PictureUrl,options=> options.MapFrom<PictureUrlResolver>());
        }
    }
}
