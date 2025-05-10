

namespace Shared.Basket.Dto
{
    public class BasketDto
    {
        public string Id { get; set; }
        ICollection<BasketItemDto> Items { get; set; } = [];
    }
}
