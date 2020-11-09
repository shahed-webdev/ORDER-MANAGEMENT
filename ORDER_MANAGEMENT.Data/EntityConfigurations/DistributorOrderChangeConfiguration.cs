using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorOrderChangeConfiguration : EntityTypeConfiguration<DistributorOrderChange>
    {
        public DistributorOrderChangeConfiguration()
        {
            ToTable("DistributorOrderChange");
            HasKey(d => d.DistributorOrderChangeID);
            Property(d => d.OrderQuantity).IsRequired();
            Property(d => d.ChangedQuantity).IsRequired();

            HasRequired(d => d.Distributor).WithMany(dis => dis.DistributorOrderChanges).HasForeignKey(d => d.DistributorID);
            HasRequired(d => d.DistributorOrder).WithMany(dis => dis.DistributorOrderChanges).HasForeignKey(d => d.DistributorOrderID);
            HasRequired(d => d.ChangeBy_Registration).WithMany(r => r.DistributorOrderChanges).HasForeignKey(d => d.ChangeBy_RegistrationID);
        }

    }
}
