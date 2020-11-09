using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class TargetAssignConfiguration : EntityTypeConfiguration<TargetAssign>
    {
        public TargetAssignConfiguration()
        {
            ToTable("TargetAssign");
            HasKey(t => t.TargetAssignID);
            Property(t => t.TargetAmount).IsRequired();
            Property(t => t.AchievedAmount).IsRequired();
            HasRequired(t => t.Target).WithMany(ta => ta.TargetAssigns).HasForeignKey(t => t.TargetID);
            HasRequired(t => t.User).WithMany(u => u.TargetAssigns).HasForeignKey(t => t.RegistrationID);
            HasRequired(t => t.AssignByUser).WithMany(u => u.TargetAssigns_To).HasForeignKey(t => t.AssignByRegistrationID);
        }
    }
}