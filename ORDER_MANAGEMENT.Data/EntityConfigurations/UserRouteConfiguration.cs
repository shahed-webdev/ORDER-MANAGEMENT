using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class UserRouteConfiguration : EntityTypeConfiguration<UserRoute>
    {
        public UserRouteConfiguration()
        {
            ToTable("UserRoute");
            HasKey(ur => ur.UserRouteID);
            HasRequired(ur => ur.User).WithMany(u => u.UserRoutes).HasForeignKey(ur => ur.RegistrationID);
            HasOptional(ur => ur.Outlet).WithMany(o => o.UserRoutes).HasForeignKey(ur => ur.OutletID);
            HasOptional(ur => ur.Distributor).WithMany(d => d.UserRoutes).HasForeignKey(ur => ur.DistributorID);
        }
    }
}
