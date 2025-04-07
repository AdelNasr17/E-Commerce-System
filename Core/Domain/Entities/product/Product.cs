
namespace Domain.Entities.product
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl {  get; set; } = null!;
        public decimal Price { get; set; }
        //Navginatial Product [One ]
        public ProductBrand ProductBrand { get; set; }
        public int BrandId { get; set; }//FK
        //Navginatial Product [One ]
        public ProductType ProductType { get; set; }
        public int TypeId { get; set; }//FK
    }
}
