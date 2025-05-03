

using Domain.Entities.Order;
using Shared.Order.Dto;

namespace Services.Mapping_Profiles
{
    public class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            return $"{_configuration["baseUrl"]}{source.Product.PictureUrl}";
        }
    }
}
