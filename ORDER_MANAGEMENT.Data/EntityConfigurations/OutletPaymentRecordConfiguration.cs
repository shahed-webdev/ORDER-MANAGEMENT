using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletPaymentRecordConfiguration : EntityTypeConfiguration<OutletPaymentRecord>
    {
        public OutletPaymentRecordConfiguration()
        {
            ToTable("OutletPaymentRecord");
            HasKey(o => o.OutletPaymentID);
            Property(o => o.Amount).IsRequired();
            Property(o => o.PaymentDate).IsRequired();
            HasRequired(o => o.Outlet).WithMany(ol => ol.OutletPaymentRecords).HasForeignKey(o => o.OutletID);
        }
    }
}
