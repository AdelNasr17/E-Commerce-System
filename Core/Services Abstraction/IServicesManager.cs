

namespace Services_Abstraction
{
    public interface IServicesManager
    {
         public IProductService ProductService { get; }
         public IBasketService  basketService { get; }
    }
}
