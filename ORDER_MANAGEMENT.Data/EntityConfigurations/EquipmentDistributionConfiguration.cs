using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentDistributionConfiguration : EntityTypeConfiguration<EquipmentDistribution>
    {
        public EquipmentDistributionConfiguration()
        {
            ToTable("EquipmentDistribution");
            HasKey(e => e.EquipmentDistributionID);
            Property(e => e.InstalledDate).IsRequired();
            Property(e => e.IsCurrent).IsRequired();
            Property(e => e.Value).IsRequired();

            HasRequired(e => e.Outlet).WithMany(o => o.EquipmentDistributions).HasForeignKey(e => e.OutletID);
            HasRequired(e => e.Equipment).WithMany(eq => eq.EquipmentDistributions).HasForeignKey(e => e.EquipmentID);
        }
    }
}
