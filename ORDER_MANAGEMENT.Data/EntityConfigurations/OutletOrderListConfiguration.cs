using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderListConfiguration : EntityTypeConfiguration<OutletOrderList>
    {
        public OutletOrderListConfiguration()
        {
            ToTable("OutletOrderList");
            HasKey(o => o.OutletOrderListID);
            Property(o => o.OrderQuantity).IsRequired();
            Property(o => o.UnitPrice).IsRequired();
            HasRequired(o => o.Product).WithMany(p => p.OutletOrderLists).HasForeignKey(o => o.ProductID);
            HasRequired(o => o.OutletOrder).WithMany(oo => oo.OutletOrderLists).HasForeignKey(o => o.OutletOrderID);
        }
    }
}
