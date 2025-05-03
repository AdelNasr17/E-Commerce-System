namespace Shared.Product.Dto
{
    public record ProductResultDto
    {
        public int Id { get; init; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public string BrandName { get; set; } = null!;
        public string TypeName { get; set; } = null!;
    }
}
