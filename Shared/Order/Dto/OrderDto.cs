

using Shared.Identity.Dto;

namespace Shared.Order.Dto
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethodId  { get; set; }
        public AddressDto Address { get; set; }=default!;   

    }
}
