using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    public class ServiceManager(IUnitOfWork _unitOfWork,
        IMapper _mapper, IBasketRepository _basketRepository,
        UserManager<User> _userManager) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductSerivce(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        private readonly Lazy<IAuthenticationService> _authService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager));

        public IProductService ProductService => _productService.Value;
        public IBasketService BasketService => _basketService.Value;
        public IAuthenticationService AuthenticationService => _authService.Value;
    }
}
