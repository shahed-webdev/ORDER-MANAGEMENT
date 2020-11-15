using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductTransferConfiguration : EntityTypeConfiguration<DepotProductTransfer>
    {
        public DepotProductTransferConfiguration()
        {
            ToTable("DepotProductTransfer");

            HasKey(a => a.DepotProductTransferId);

            HasRequired(a => a.Depot)
                .WithMany(r => r.DepotProductTransfers)
                .HasForeignKey(a => a.DepotId);

            HasRequired(a => a.Product)
                .WithMany(r => r.DepotProductTransfers)
                .HasForeignKey(a => a.ProductID);

            HasRequired(a => a.TransferByRegistration)
                .WithMany(r => r.DepotProductTransfers)
                .HasForeignKey(a => a.TransferByRegistrationID);

        }
    }
}