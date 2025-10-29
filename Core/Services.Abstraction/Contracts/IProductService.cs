using Shared.Dtos;
using Shared.Enums;

namespace Services.Abstraction.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? typeId, int? brandId, ProductSortingOptains sort);
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        Task<ProductResultDto?> GetProductByIdAsync(int id);
    }
}
