using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class RegistrationConfiguration : EntityTypeConfiguration<Registration>
    {
        public RegistrationConfiguration()
        {
            ToTable("Registration");
            HasKey(r => r.RegistrationID);
            Property(r => r.UserName).IsRequired().HasMaxLength(80);
            Property(r => r.Type).IsRequired().HasMaxLength(20);
            Property(r => r.Designation).HasMaxLength(128);
            Property(r => r.Name).IsRequired().HasMaxLength(128);
            Property(r => r.NID).HasMaxLength(120);
            Property(r => r.FatherName).HasMaxLength(150);
            Property(r => r.MotherName).HasMaxLength(150);
            Property(r => r.PresentAddress).HasMaxLength(256);
            Property(r => r.PermanentAddress).HasMaxLength(256);
            Property(r => r.OfficeContact).HasMaxLength(50);
            Property(r => r.PersonalContact).HasMaxLength(50);
            Property(r => r.HomeContact).HasMaxLength(50);
            Property(r => r.EmergencyContact).HasMaxLength(50);
            Property(r => r.OfficeEmail).HasMaxLength(50);
            Property(r => r.PersonalEmail).HasMaxLength(50);
            Property(r => r.BloodGroup).HasMaxLength(50);
            Property(r => r.PS).HasMaxLength(50);
            HasOptional(u => u.User).WithRequired(s => s.Registration);
        }
    }
}