using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class TargetConfiguration : EntityTypeConfiguration<Target>
    {
        public TargetConfiguration()
        {
            ToTable("Target");
            HasKey(t => t.TargetID);
            Property(t => t.Target_Title).IsRequired().HasMaxLength(50);
            Property(t => t.StartDate).IsRequired();
            Property(t => t.EndDate).IsRequired();
            Property(t => t.Total_TargetAmount).IsRequired();
            Property(t => t.Total_AchievedAmount).IsRequired();
            HasRequired(t => t.Registration).WithMany(r => r.Target_Creates).HasForeignKey(t => t.CreatedByRegistrationID);
        }
    }
}