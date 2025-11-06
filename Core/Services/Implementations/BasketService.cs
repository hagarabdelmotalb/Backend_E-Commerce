using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exepctions;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;

namespace Services.Implementations
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            var CreateOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return CreateOrUpdateBasket is not null ? _mapper.Map<BasketDto>(CreateOrUpdateBasket) 
                : throw new Exception("Can not create or update the Basket");
        }

        public Task<bool> DeleteBasketAsync(string Id)
            => _basketRepository.DeleteBasketAsync(Id);

        public async Task<BasketDto> GetBasketAsync(string Id)
        {
            var basket = await _basketRepository.GetBasketAsync(Id);
            return basket is null ? throw new BasketNotFoundException(Id)
                : _mapper.Map<BasketDto>(basket);
        }
    }
}
