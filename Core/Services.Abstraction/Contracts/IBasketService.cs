using Shared.Dtos.BasketModule;

namespace Services.Abstraction.Contracts
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string Id);
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
        Task<bool> DeleteBasketAsync(string Id);
    }
}
