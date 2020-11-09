using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorOrderConfiguration : EntityTypeConfiguration<DistributorOrder>
    {
        public DistributorOrderConfiguration()
        {
            ToTable("DistributorOrder");
            HasKey(d => d.DistributorOrderID);
            HasRequired(d => d.Distributor).WithMany(dis => dis.DistributorOrders).HasForeignKey(d => d.DistributorID);
            HasRequired(d => d.OrderBy_Registration).WithMany(r => r.OrderBy_DistributorOrders).HasForeignKey(d => d.OrderBy_RegistrationID);
            HasRequired(d => d.ApproveBy_Registration).WithMany(r => r.ApproveBy_DistributorOrders).HasForeignKey(d => d.ApproveBy_RegistrationID);
        }
    }
}
