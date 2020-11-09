using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkConfiguration : EntityTypeConfiguration<PageLink>
    {
        public PageLinkConfiguration()
        {
            ToTable("PageLink");
            HasKey(p => p.LinkID);
            HasRequired(p => p.LinkCategory).WithMany(c => c.PageLinks).HasForeignKey(p => p.LinkCategoryID);
            Property(p => p.RoleId).IsRequired();
            Property(p => p.Controller).HasMaxLength(128).IsRequired();
            Property(p => p.Action).HasMaxLength(128).IsRequired();
            Property(p => p.Title).HasMaxLength(128).IsRequired();
        }
    }
}
