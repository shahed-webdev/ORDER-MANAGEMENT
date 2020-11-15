using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorConfiguration : EntityTypeConfiguration<Distributor>
    {
        public DistributorConfiguration()
        {
            ToTable("Distributor");
            HasKey(d => d.DistributorID);
            Property(d => d.Name).IsRequired().HasMaxLength(128);
            Property(d => d.DueRangeLimit).IsRequired();
            Property(d => d.Lat).IsRequired().HasMaxLength(100);
            Property(d => d.Lon).IsRequired().HasMaxLength(100);
            Property(d => d.Address).IsRequired().HasMaxLength(500);
            HasRequired(d => d.Territory).WithMany(t => t.Distributors).HasForeignKey(d => d.TerritoryID);
            HasRequired(d => d.ReportTo_User).WithMany(u => u.Distributors).HasForeignKey(d => d.ReportTo_RegistrationID);
            HasOptional(d => d.Depot).WithMany(o => o.Distributors).HasForeignKey(d => d.DepotId);
        }
    }
}
