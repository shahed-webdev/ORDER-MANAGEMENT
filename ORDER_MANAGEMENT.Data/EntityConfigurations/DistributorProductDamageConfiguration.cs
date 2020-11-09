using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorProductDamageConfiguration : EntityTypeConfiguration<DistributorProductDamage>
    {
        public DistributorProductDamageConfiguration()
        {
            ToTable("DistributorProductDamage");
            HasKey(d => d.DistributorProductDamageID);
            Property(d => d.DamageQuantity).IsRequired();
            Property(d => d.DamageDate).IsRequired();
            Property(d => d.InsertDate).IsRequired();

            HasRequired(d => d.Distributor).WithMany(dis => dis.DistributorProductDamages).HasForeignKey(d => d.DistributorID);
            HasRequired(d => d.Product).WithMany(p => p.DistributorProductDamages).HasForeignKey(d => d.ProductID);


        }
    }
}
