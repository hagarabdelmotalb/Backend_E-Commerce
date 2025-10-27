using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Shared.Dtos;

namespace Services.Implementations
{
    public class ProductSerivce(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            var brandResult =  _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandResult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var Products =  await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            var ProductResult =  _mapper.Map<IEnumerable<ProductResultDto>>(Products);
            return ProductResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var TypeResult =  _mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return TypeResult;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(id);
            var productResult =  _mapper.Map<ProductResultDto>(product);
            return productResult;
        }
    }
}
