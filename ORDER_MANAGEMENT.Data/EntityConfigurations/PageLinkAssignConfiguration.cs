using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkAssignConfiguration : EntityTypeConfiguration<PageLinkAssign>
    {
        public PageLinkAssignConfiguration()
        {
            ToTable("PageLinkAssign");
            HasKey(p => p.LinkAssignID);
            HasRequired(p => p.PageLink).WithMany(l => l.PageLinkAssigns).HasForeignKey(p => p.LinkID);
            HasRequired(p => p.Registration).WithMany(r => r.PageLinkAssigns).HasForeignKey(p => p.RegistrationID);

        }
    }
}
