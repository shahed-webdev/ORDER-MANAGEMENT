using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfiguration()
        {
            ToTable("ProductCategory");
            HasKey(p => p.ProductCategoryID);
            Property(p => p.ProductCategoryName).HasMaxLength(128).IsRequired();
            Property(p => p.ProductMainCategoryID).IsRequired();
            HasRequired(p => p.ProductMainCategory).WithMany(m => m.ProductCategories)
                .HasForeignKey(p => p.ProductMainCategoryID);
        }
    }
}