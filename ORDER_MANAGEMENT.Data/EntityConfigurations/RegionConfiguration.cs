using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration()
        {
            ToTable("Region");
            HasKey(u => u.RegionID);
            Property(r => r.RegionName).HasMaxLength(100).IsRequired();
        }
    }
}