

namespace Services
{
    public sealed class ServicesManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository) : IServicesManager
    {
        private readonly Lazy< IProductService> _ProductService= new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _ProductService.Value;


        private readonly Lazy<IBasketService> _BasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService basketService => _BasketService.Value;
    }
}
