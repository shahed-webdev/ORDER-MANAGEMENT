using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class HierarchyConfiguration : EntityTypeConfiguration<Organization_hierarchy>
    {
        public HierarchyConfiguration()
        {
            ToTable("Organization_hierarchy");
            HasKey(h => h.HierarchyID);
            Property(h => h.HierarchyName).IsRequired().HasMaxLength(50);
            Property(h => h.Rank).IsRequired();
        }
    }
}