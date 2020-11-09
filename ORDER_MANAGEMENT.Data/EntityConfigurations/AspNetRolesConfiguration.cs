using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class AspNetRolesConfiguration : EntityTypeConfiguration<AspNetRoles>
    {
        public AspNetRolesConfiguration()
        {
            ToTable("AspNetRoles");
            HasKey(r => r.Id);
        }
    }
}
