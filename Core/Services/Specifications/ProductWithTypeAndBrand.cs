using Domain.Contracts;
using Domain.Entities.ProductModule;
using Shared;
using Shared.Enums;

namespace Services.Specifications
{
    public class ProductWithTypeAndBrand : BaseSecifications<Product, int>
    {
        public ProductWithTypeAndBrand(ProductSpecificationParameters parameters) :
            base(p => (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) &&
                      (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)&&
                      (string.IsNullOrEmpty(parameters.search) || p.Name.ToLower().Contains(parameters.search.ToLower())))
        {
            AddIncludes(p => p.ProductType);
            AddIncludes(p => p.ProductBrand);
            switch (parameters.Sort)
            {
                case ProductSortingOptains.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptains.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptains.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptains.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }
        public ProductWithTypeAndBrand(int id) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductType);
            AddIncludes(p => p.ProductBrand);
        }
    }
}
