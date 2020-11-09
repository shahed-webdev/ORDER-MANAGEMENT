using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductMainCategory
    {

        public ProductMainCategory()
        {
            this.ProductCategories = new HashSet<ProductCategory>();
        }
        public int ProductMainCategoryID { get; set; }

        [Required(ErrorMessage = "Category Name required !!")]
        public string ProductMainCategoryName { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
