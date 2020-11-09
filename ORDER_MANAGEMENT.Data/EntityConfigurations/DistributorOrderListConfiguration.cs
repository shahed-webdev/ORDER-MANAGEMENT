using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorOrderListConfiguration : EntityTypeConfiguration<DistributorOrderList>
    {
        public DistributorOrderListConfiguration()
        {
            ToTable("DistributorOrderList");
            HasKey(d => d.DistributorOrderListID);
            Property(d => d.ProductID).IsRequired();
            Property(d => d.OrderQuantity).IsRequired();
            Property(d => d.UnitPrice).IsRequired();

            HasRequired(d => d.DistributorOrder).WithMany(d => d.DistributorOrderLists).HasForeignKey(d => d.DistributorOrderID);

        }
    }
}
