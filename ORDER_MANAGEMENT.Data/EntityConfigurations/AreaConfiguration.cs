using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class AreaConfiguration : EntityTypeConfiguration<Area>
    {
        public AreaConfiguration()
        {
            ToTable("Area");
            HasKey(a => a.AreaID);
            Property(a => a.AreaName).HasMaxLength(100).IsRequired();
            HasRequired(a => a.Region).WithMany(r => r.Areas).HasForeignKey(a => a.RegionID);
        }
    }
}