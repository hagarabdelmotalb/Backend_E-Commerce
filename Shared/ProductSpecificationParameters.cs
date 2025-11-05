using Shared.Enums;

namespace Shared
{
    public class ProductSpecificationParameters
    {
        private const int defaultPageSize = 10;
        private const int maxPageSize = 50;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptains Sort { get; set; }
        public string? search { get; set; }
        public int PageIndex { get; set; }
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }

    }
}
