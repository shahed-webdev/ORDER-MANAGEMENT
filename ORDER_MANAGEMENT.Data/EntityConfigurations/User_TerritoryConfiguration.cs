using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class User_TerritoryConfiguration : EntityTypeConfiguration<User_Territory>
    {
        public User_TerritoryConfiguration()
        {
            HasKey(ut => new { ut.RegistrationID, ut.TerritoryID });
            HasRequired(t => t.Territory).WithMany(t => t.user_Territories).HasForeignKey(t => t.TerritoryID);
            HasRequired(u => u.User).WithMany(u => u.user_Territories).HasForeignKey(u => u.RegistrationID);
        }
    }
}