using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletProductDamageConfiguration : EntityTypeConfiguration<OutletProductDamage>
    {
        public OutletProductDamageConfiguration()
        {
            ToTable("OutletProductDamage");
            HasKey(o => o.OutletProductDamageID);
            Property(o => o.DamageQuantity).IsRequired();
            Property(o => o.DamageDate).IsRequired();
            HasRequired(o => o.Outlet).WithMany(pd => pd.OutletProductDamages).HasForeignKey(o => o.OutletID);
            HasRequired(o => o.Product).WithMany(p => p.OutletProductDamages).HasForeignKey(o => o.OutletID);


        }
    }
}
