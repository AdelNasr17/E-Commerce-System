

using Shared.Order.Dto;

namespace Services_Abstraction
{
    public interface IOrderService
    {
        //Creating Order Will Take Basket Id , Shipping Address , Delivery Method Id , Customer Email  And
        //Return Order Details (Id , UserName , OrderDate , Items (Product Name - Picture Url - Price - Quantity)
        //, Address , Delivery Method Name , Order Status Value , Sub Total , Total Price  )

        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);


        //Get DeliveryMethod 
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();
        //Get All Order By Email 
        Task<IEnumerable<OrderToReturnDto>> GetAllOrderByEmailAsync(string email);



        //get Order By Id 
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);


    }
}
