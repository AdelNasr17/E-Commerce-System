
namespace Domain.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }//Giud :Create From Client

        public ICollection<BasketItem> BasketItems { get; set; } = [];
    }
}
