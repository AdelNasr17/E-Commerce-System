

namespace Services
{
    public sealed class ServicesManager : IServicesManager
    {
        private readonly Lazy< IProductService> _ProductService;
        private readonly Lazy< IMapper> _mapper;

        public ServicesManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _ProductService = new Lazy<IProductService>(()=> new ProductService(unitOfWork,mapper));
           
        }
        public IProductService ProductService => _ProductService.Value;
    }
}
