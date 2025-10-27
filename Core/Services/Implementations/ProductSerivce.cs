using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Shared.Dtos;

namespace Services.Implementations
{
    public class ProductSerivce(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            _unitOfWork.GetRepository<ProductBrand,int>();
        }

        public Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            _unitOfWork.GetRepository<ProductBrand,int>();
        }

        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
