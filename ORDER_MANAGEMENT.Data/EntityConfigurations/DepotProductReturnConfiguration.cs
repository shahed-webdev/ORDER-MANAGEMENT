using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductReturnConfiguration : EntityTypeConfiguration<DepotProductReturn>
    {
        public DepotProductReturnConfiguration()
        {
            ToTable("DepotProductReturn");

            HasKey(a => a.DepotProductReturnId);

            HasRequired(a => a.Depot)
                .WithMany(r => r.DepotProductReturns)
                .HasForeignKey(a => a.DepotId);

            HasRequired(a => a.Product)
                .WithMany(r => r.DepotProductReturns)
                .HasForeignKey(a => a.ProductID);

            HasRequired(a => a.ReturnByRegistration)
                .WithMany(r => r.DepotProductReturns)
                .HasForeignKey(a => a.ReturnByRegistrationID);
        }

    }
}