global using Microsoft.Extensions.Configuration;
using Shared.Product.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping_Profiles
{
    internal class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        string IValueResolver<Product, ProductResultDto, string>.Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl)) 
                return string.Empty;           

            return $"{_configuration["baseUrl"]}{source.PictureUrl}";
            
        }
    }
}
