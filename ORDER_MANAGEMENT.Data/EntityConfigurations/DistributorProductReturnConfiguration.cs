using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorProductReturnConfiguration : EntityTypeConfiguration<DistributorProductReturn>
    {
        public DistributorProductReturnConfiguration()
        {
            ToTable("DistributorProductReturn");
            HasKey(d => d.DistributorProductReturnID);
            HasRequired(d => d.ReturnBy_Registration).WithMany(r => r.ReturnBy_DistributorProductReturns).HasForeignKey(d => d.ReturnBy_RegistrationID);
            HasRequired(d => d.ApproveBy_Registration).WithMany(r => r.ApproveBy_DistributorProductReturns).HasForeignKey(d => d.ApproveBy_RegistrationID);

        }
    }
}
