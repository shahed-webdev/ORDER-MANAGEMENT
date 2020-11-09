using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderConfiguration : EntityTypeConfiguration<OutletOrder>
    {
        public OutletOrderConfiguration()
        {
            ToTable("OutletOrder");
            HasKey(o => o.OutletOrderID);
            Property(o => o.Lat).IsRequired().HasMaxLength(100);
            Property(o => o.Lon).IsRequired().HasMaxLength(100);
            HasRequired(o => o.Outlet).WithMany(oo => oo.OutletOrders).HasForeignKey(o => o.OutletID);
            HasRequired(o => o.Distributor).WithMany(d => d.OutletOrders).HasForeignKey(o => o.DistributorID);
            HasRequired(d => d.OrderBy_Registration).WithMany(r => r.OrderBy_OutletOrders).HasForeignKey(d => d.OrderBy_RegistrationID);
            HasRequired(d => d.ApproveBy_Registration).WithMany(r => r.ApproveBy_OutletOrders).HasForeignKey(d => d.ApproveBy_RegistrationID);
        }
    }
}
