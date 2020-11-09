using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderChangeConfiguration : EntityTypeConfiguration<OutletOrderChange>
    {
        public OutletOrderChangeConfiguration()
        {
            ToTable("OutletOrderChange");
            HasKey(o => o.OutletOrderChangeID);
            HasRequired(o => o.OutletOrder).WithMany(oo => oo.OutletOrderChanges).HasForeignKey(o => o.OutletOrderID);
            Property(o => o.Reason).HasMaxLength(500);
            Property(o => o.OrderQuantity).IsRequired();
            Property(o => o.ChangedQuantity).IsRequired();
            HasRequired(o => o.Product).WithMany(p => p.OutletOrderChanges).HasForeignKey(o => o.ProductID);
        }
    }
}
