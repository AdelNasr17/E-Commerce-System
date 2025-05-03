
using Domain.Entities.Basket;
using Domain.Entities.Order;
using Domain.Exceptions;
using Services.Specifications.OrderSpecifications;
using Shared.Identity.Dto;
using Shared.Order.Dto;
using System;

namespace Services
{
    public class OrderService(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
         //Mappimg Address To OrderAdress 
         var OrderAddress=  _mapper.Map<AddressDto,OrderAddress>(orderDto.Address);

            // Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);

            //Create OrderItem List 
            List<OrderItem> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            
            foreach(var item in Basket.BasketItems)
            {
                var product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item, product));
            }

            //Get DeliveryMethod
            var DeliveryMethod= await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            // Calc SubTotal 
            var subTotal = orderItems.Sum(I=> I.Quantity* I.Price);

            var Order = new Order(email, OrderAddress, DeliveryMethod, orderItems, subTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
          await  _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order,OrderToReturnDto>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem()
            {
                Price = item.Price,
                Quantity = item.Quantity,
                Product = new ProductItemOrdered() { ProductId = product.Id, PictureUrl = product.PictureUrl, ProductName = product.Name },
            };
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
           var deliverMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>,IEnumerable<DeliveryMethodDto>>(deliverMethod);

        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderByEmailAsync(string email)
        {
            var Spac = new OrderSpecification(email);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spac);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Order);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var Spac = new OrderSpecification(id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spac);
            return _mapper.Map<Order,OrderToReturnDto>(Order);
        }
    }
}
