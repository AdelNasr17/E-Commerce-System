

using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public sealed class ServicesManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepository basketRepository,UserManager<ApplicationUser> userManager,IConfiguration configuration) : IServicesManager
    {
        private readonly Lazy< IProductService> _ProductService= new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        public IProductService ProductService => _ProductService.Value;


        private readonly Lazy<IBasketService> _BasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService basketService => _BasketService.Value;

        private readonly Lazy<IAuthenticationService>  _authenticationService= new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,configuration,mapper));
        public IAuthenticationService authenticationService =>_authenticationService.Value;
    }
}
