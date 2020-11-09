using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class TerritoryConfiguration : EntityTypeConfiguration<Territory>
    {
        public TerritoryConfiguration()
        {
            ToTable("Territory");
            HasKey(t => t.TerritoryID);
            Property(t => t.TerritoryName).HasMaxLength(100).IsRequired();
            HasRequired(t => t.Area).WithMany(t => t.Territorys).HasForeignKey(d => d.AreaID);
        }
    }
}