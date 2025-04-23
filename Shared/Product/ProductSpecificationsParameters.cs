using System.ComponentModel.DataAnnotations;

namespace Shared.Product
{
    public class ProductSpecificationsParameters
    {

        public int? BrandId { get; set; }
        public int? typeId { get; set; }
        public ProductSortOptions? Sort { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        private int _PageSize = DefaultPageSize;
        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = value > MaxPageSize ? MaxPageSize : value;
        }


    }

    public enum ProductSortOptions
    {
        
        NameAsc,
        NameDesc,
        priceAsc,
        PriceDesc,
    }
}
