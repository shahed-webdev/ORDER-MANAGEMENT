using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkCategoryConfiguration : EntityTypeConfiguration<PageLinkCategory>
    {
        public PageLinkCategoryConfiguration()
        {
            ToTable("PageLinkCategory");
            HasKey(p => p.LinkCategoryID);
            Property(p => p.Category).IsRequired().HasMaxLength(128);
            Property(p => p.IconClass).HasMaxLength(128);
        }
    }
}
