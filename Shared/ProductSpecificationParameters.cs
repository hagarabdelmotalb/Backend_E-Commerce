using Shared.Enums;

namespace Shared
{
    public class ProductSpecificationParameters
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptains Sort { get; set; }
        public string? search { get; set; }
    }
}
