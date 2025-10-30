using Domain.Entities.ProductModule;
using Shared;

namespace Services.Specifications
{
    public class ProductCountspecifications : BaseSecifications<Product,int>
    {
        public ProductCountspecifications(ProductSpecificationParameters parameters)
            : base(p => (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) &&
                      (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId) &&
                      (string.IsNullOrEmpty(parameters.search) || p.Name.ToLower().Contains(parameters.search.ToLower())))
        {
        }
    }
}
