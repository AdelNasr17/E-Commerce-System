

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Order
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod,ICollection<OrderItem> orderItems, decimal subTotal)
        {
            UserEmail = userEmail;
            Address = address;
            Items=orderItems;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
        }

        public string UserEmail { get; set; } = default!;
        public OrderAddress Address { get; set; } = default!;
        public OrderStatus Status { get; set; }
        public decimal SubTotal { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
        //[NotMapped]
        //public decimal Total { get => SubTotal + DeliveryMethod.Price; }
        public int DeliveryMethodId { get; set; }//FK
        public ICollection<OrderItem> Items { get; set; } = [];


        public decimal GetTotal()=> SubTotal + DeliveryMethod.Price;
    }
}
