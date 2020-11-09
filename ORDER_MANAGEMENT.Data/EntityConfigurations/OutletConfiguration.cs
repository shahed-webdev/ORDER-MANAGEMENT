using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletConfiguration : EntityTypeConfiguration<Outlet>
    {
        public OutletConfiguration()
        {
            ToTable("Outlet");
            HasKey(o => o.OutletID);
            Property(o => o.OutletName).IsRequired().HasMaxLength(500);
            Property(o => o.Lat).IsRequired().HasMaxLength(200);
            Property(o => o.Lon).IsRequired().HasMaxLength(200);
            Property(o => o.ProprietorName).IsRequired().HasMaxLength(150);
            Property(o => o.Address).IsRequired().HasMaxLength(1024);
            Property(o => o.Phone).IsRequired().HasMaxLength(50);
            Property(o => o.Email).HasMaxLength(150);
            HasRequired(o => o.Territory).WithMany(t => t.Outlets).HasForeignKey(o => o.TerritoryID);
            HasRequired(o => o.Distributor).WithMany(d => d.Outlets).HasForeignKey(o => o.DistributorID);
        }
    }
}
