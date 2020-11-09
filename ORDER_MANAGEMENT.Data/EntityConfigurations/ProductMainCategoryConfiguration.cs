using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductMainCategoryConfiguration : EntityTypeConfiguration<ProductMainCategory>
    {
        public ProductMainCategoryConfiguration()
        {
            ToTable("ProductMainCategory");
            HasKey(p => p.ProductMainCategoryID);
            Property(p => p.ProductMainCategoryName).HasMaxLength(128).IsRequired();
        }
    }

}
