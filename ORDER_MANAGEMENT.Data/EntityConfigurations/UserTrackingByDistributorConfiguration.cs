using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class UserTrackingByDistributorConfiguration : EntityTypeConfiguration<UserTrackingByDistributor>
    {
        public UserTrackingByDistributorConfiguration()
        {
            ToTable("UserTrackingByDistributor");
            HasKey(t => t.TrackingByDistributorID);
            Property(u => u.CheckIn_Lat).IsRequired().HasMaxLength(100);
            Property(u => u.CheckIn_Lon).IsRequired().HasMaxLength(100);
            Property(u => u.TrackingDate).IsRequired();
            Property(u => u.TrackingDateTime).IsRequired();
            HasRequired(t => t.User).WithMany(u => u.UserTrackingByDistributors).HasForeignKey(t => t.RegistrationID);
            HasRequired(t => t.Distributor).WithMany(u => u.UserTrackingByDistributors).HasForeignKey(t => t.DistributorID);
        }
    }
}
