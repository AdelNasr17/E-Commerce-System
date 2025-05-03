

namespace Persistence.Data.Configurations.product
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId);

            builder.HasOne(P => P.ProductType)
                  .WithMany()
                  .HasForeignKey(P => P.TypeId);

            builder.Property(p => p.Price).HasColumnType("decimal(18,3)");
        }
    }
}
