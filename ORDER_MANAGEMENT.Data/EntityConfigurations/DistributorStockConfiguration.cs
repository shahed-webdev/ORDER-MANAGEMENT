using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class DistributorStockConfiguration : EntityTypeConfiguration<DistributorStock>
    {
        public DistributorStockConfiguration()
        {
            ToTable("DistributorStock");
            HasKey(d => d.DistributorStockID);
        }
    }
}
