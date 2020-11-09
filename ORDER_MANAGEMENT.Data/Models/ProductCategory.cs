using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
        public int ProductCategoryID { get; set; }
        public int ProductMainCategoryID { get; set; }

        [Required(ErrorMessage = "Category Name required !!")]
        public string ProductCategoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ProductMainCategory ProductMainCategory { get; set; }
    }
}