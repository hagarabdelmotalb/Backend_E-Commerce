using Domain.Contracts;
using Domain.Entities.ProductModule;
using Shared.Enums;

namespace Services.Specifications
{
    public class ProductWithTypeAndBrand : BaseSecifications<Product, int>
    {
        public ProductWithTypeAndBrand(int? typeId, int? brandId, ProductSortingOptains sort) :
            base(p => (!typeId.HasValue || p.TypeId == typeId) &&
                      (!brandId.HasValue || p.BrandId == brandId))
        {
            AddIncludes(p => p.ProductType);
            AddIncludes(p => p.ProductBrand);
            switch (sort)
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
