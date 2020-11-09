using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class UserTrackingByOutletConfiguration : EntityTypeConfiguration<UserTrackingByOutlet>
    {
        public UserTrackingByOutletConfiguration()
        {
            ToTable("UserTrackingByOutlet");
            HasKey(t => t.TrackingByOutletID);
            Property(u => u.CheckIn_Lat).IsRequired().HasMaxLength(100);
            Property(u => u.CheckIn_Lon).IsRequired().HasMaxLength(100);
            Property(u => u.TrackingDate).IsRequired();
            Property(u => u.TrackingDateTime).IsRequired();
            HasRequired(t => t.User).WithMany(u => u.UserTrackingByOutlets).HasForeignKey(t => t.RegistrationID);
            HasRequired(t => t.Outlet).WithMany(u => u.UserTrackingByOutlets).HasForeignKey(t => t.OutletID);
        }
    }
}
