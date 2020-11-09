using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class RouteConfiguration : EntityTypeConfiguration<Route>
    {
        public RouteConfiguration()
        {
            ToTable("Route");
            HasKey(r => r.RouteID);
            Property(r => r.RouteName).IsRequired().HasMaxLength(128);

        }
    }
}
