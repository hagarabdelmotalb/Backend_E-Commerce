using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Domain.Exepctions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared;
using Shared.Dtos;
using Shared.Enums;

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

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var specification = new ProductWithTypeAndBrand(parameters);
            var Products =  await _unitOfWork.GetRepository<Product,int>().GetAllAsync(specification);
            var productResult =  _mapper.Map<IEnumerable<ProductResultDto>>(Products);
            var pageSize = Products.Count();
            var countSpecification = new ProductCountspecifications(parameters);
            var totalCount = await _unitOfWork.GetRepository<Product,int>().CountAsync(countSpecification);
            return new PaginatedResult<ProductResultDto>(parameters.PageIndex,pageSize, totalCount, productResult);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var TypeResult =  _mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return TypeResult;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithTypeAndBrand(id);
            var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(specification);
            //var productResult =  _mapper.Map<ProductResultDto>(product);
            //return productResult;
            return product is null ? throw new ProductNotFoundException(id)
                : _mapper.Map<ProductResultDto>(product);
        }

    }
}
