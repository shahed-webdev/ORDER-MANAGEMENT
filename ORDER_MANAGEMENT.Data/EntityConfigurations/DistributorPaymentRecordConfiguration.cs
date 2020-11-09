using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorPaymentRecordConfiguration : EntityTypeConfiguration<DistributorPaymentRecord>
    {
        public DistributorPaymentRecordConfiguration()
        {
            ToTable("DistributorPaymentRecord");
            HasKey(d => d.DistributorPaymentID);
            Property(d => d.Amount).IsRequired();
            Property(d => d.PaymentDate).IsRequired();
            HasRequired(d => d.Distributor).WithMany(dis => dis.DistributorPaymentRecords).HasForeignKey(d => d.DistributorID);

        }
    }
}
