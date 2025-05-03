

using Shared.Identity.Dto;

namespace Shared.Order.Dto
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } 
        public AddressDto Address { get; set; } = default!;
        public string Status { get; set; }= default!;
        public decimal SubTotal { get; set; }
       
        public string DeliveryMethod { get; set; }=default!;
       
        public ICollection<OrderItemDto> Items { get; set; } = [];


        public decimal Total { get; set; }
    }
}
