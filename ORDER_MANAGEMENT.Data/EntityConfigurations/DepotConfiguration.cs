using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotConfiguration : EntityTypeConfiguration<Depot>
    {
        public DepotConfiguration()
        {
            ToTable("Depot");

            HasKey(a => a.DepotId);

            Property(a => a.DepotName)
                .HasMaxLength(128)
                .IsRequired();

            HasRequired(a => a.Region)
                .WithMany(r => r.Depots)
                .HasForeignKey(a => a.RegionID);

        }
    }
}