using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotStockConfiguration : EntityTypeConfiguration<DepotStock>
    {
        public DepotStockConfiguration()
        {
            ToTable("DepotStock");

            HasKey(a => a.DepotStockId);

            HasRequired(a => a.Depot)
                .WithMany(r => r.DepotStocks)
                .HasForeignKey(a => a.DepotId);

            HasRequired(a => a.Product)
                .WithMany(r => r.DepotStocks)
                .HasForeignKey(a => a.ProductID);
        }
    }
}