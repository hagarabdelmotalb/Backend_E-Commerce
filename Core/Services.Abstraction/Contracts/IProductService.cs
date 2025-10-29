using Shared;
using Shared.Dtos;
using Shared.Enums;

namespace Services.Abstraction.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        Task<ProductResultDto?> GetProductByIdAsync(int id);
    }
}
