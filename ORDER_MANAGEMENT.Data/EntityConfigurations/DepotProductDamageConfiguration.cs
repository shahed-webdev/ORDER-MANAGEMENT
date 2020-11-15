using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductDamageConfiguration : EntityTypeConfiguration<DepotProductDamage>
    {
        public DepotProductDamageConfiguration()
        {
            ToTable("DepotProductDamage");

            HasKey(a => a.DepotProductDamageId);

            HasRequired(a => a.Depot)
                .WithMany(r => r.DepotProductDamages)
                .HasForeignKey(a => a.DepotId);

            HasRequired(a => a.Product)
                .WithMany(r => r.DepotProductDamages)
                .HasForeignKey(a => a.ProductID);

            HasRequired(a => a.DamageByRegistration)
                .WithMany(r => r.DepotProductDamages)
                .HasForeignKey(a => a.DamageByRegistrationID);
        }
    }
}