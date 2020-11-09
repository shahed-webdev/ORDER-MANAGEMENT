using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class RouteDaysConfiguration : EntityTypeConfiguration<RouteDays>
    {
        public RouteDaysConfiguration()
        {
            ToTable("RouteDays");
            HasKey(r => r.RouteDayID);
            HasRequired(rd => rd.Route).WithMany(r => r.RouteDays).HasForeignKey(rd => rd.RouteID);

        }
    }
}
