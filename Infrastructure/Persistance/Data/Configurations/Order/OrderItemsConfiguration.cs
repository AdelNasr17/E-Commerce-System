


namespace Persistence.Data.Configurations.Order
{
    internal class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.Property(oi => oi.Price).HasColumnType("decimal(8,2)");

            builder.OwnsOne(oi => oi.Product);
        }
    }
}
