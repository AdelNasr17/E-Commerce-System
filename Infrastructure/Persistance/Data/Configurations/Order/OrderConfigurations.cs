
namespace Persistence.Data.Configurations.Order
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Domain.Entities.Order.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(o => o.SubTotal).HasColumnType("decimal(8,2)");
            builder.HasMany(o => o.Items).WithOne();
            builder.HasOne(d=>d.DeliveryMethod).WithMany().HasForeignKey(o=>o.DeliveryMethodId);
            builder.OwnsOne(o => o.Address);

        }
    }
}
