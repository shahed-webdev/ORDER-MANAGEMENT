using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product");
            HasKey(p => p.ProductID);
            Property(p => p.ProductName).HasMaxLength(128).IsRequired();
            HasRequired(p => p.ProductCategory).WithMany(c => c.Products).HasForeignKey(p => p.ProductCategoryID);
        }
    }
}