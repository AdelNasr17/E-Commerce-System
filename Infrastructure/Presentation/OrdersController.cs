

using Domain.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.Order.Dto;
using System.Security.Claims;

namespace Presentation
{
    [Authorize]
    public class OrdersController(IServicesManager _servicesManager):ApiBaseController
    {
        //Create order
        
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder (OrderDto order)
        {
            

            var Order =await _servicesManager.orderService.CreateOrderAsync(order, GetEmailFromToken());
            return Ok(Order);
        }

        //Get DeliveryMethod 
        [AllowAnonymous]
        [HttpGet("DeliverMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
        {
            var DeliverMethod = await _servicesManager.orderService.GetDeliveryMethodAsync();
            return Ok(DeliverMethod);
        }

        //Get All Order By Email 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrderByEmail()
        {
            var Orders = await _servicesManager.orderService.GetAllOrderByEmailAsync(GetEmailFromToken());
            return Ok(Orders);
        }

        //get Order By Id 
       
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var Order = await _servicesManager.orderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
