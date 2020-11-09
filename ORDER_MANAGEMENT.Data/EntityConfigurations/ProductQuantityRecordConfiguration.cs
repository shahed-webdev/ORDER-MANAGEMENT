using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    class ProductQuantityRecordConfiguration : EntityTypeConfiguration<ProductQuantityRecord>
    {
        public ProductQuantityRecordConfiguration()
        {
            ToTable("ProductQuantityRecord");
            HasKey(pq => pq.ProductQuantityRecordID);
            Property(pq => pq.Quantity).IsRequired();
            HasRequired(pq => pq.Product).WithMany(p => p.ProductQuantityRecords).HasForeignKey(pq => pq.ProductID);
        }
    }
}
