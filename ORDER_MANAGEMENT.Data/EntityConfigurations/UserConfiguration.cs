using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.RegistrationID);
            HasRequired(u => u.Organization_hierarchy).WithMany(h => h.Users).HasForeignKey(u => u.HierarchyID);
            HasRequired(u => u.Upper_Registration).WithMany(r => r.Users_Under).HasForeignKey(u => u.Upper_RegistrationID);
            HasOptional(u => u.Route).WithMany(r => r.Users).HasForeignKey(u => u.RouteID);
            HasOptional(u => u.Distributor).WithMany(d => d.Users).HasForeignKey(u => u.DistributorID);
        }
    }
}
