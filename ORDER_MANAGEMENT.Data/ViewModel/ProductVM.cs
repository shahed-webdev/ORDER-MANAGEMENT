using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductVM
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Category Required !!")]
        public int ProductCategoryID { get; set; }
        public int ProductMainCategoryID { get; set; }

        [Required(ErrorMessage = "Product Name Required !!")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Code Required !!")]
        [Unique(ErrorMessage = "Product Code already exist !!", TargetModelType = typeof(Product), TargetPropertyName = "ProductCode")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "SKU Required !!")]
        [Unique(ErrorMessage = "SKU already exist !!", TargetModelType = typeof(Product), TargetPropertyName = "SKU")]
        public int SKU { get; set; }

        [Required(ErrorMessage = "Size Required !!")]
        public string Size { get; set; }

        [Required(ErrorMessage = "MRP Required !!")]
        public double? MRP { get; set; }
        public string Description { get; set; }
        public byte[] ProductImage { get; set; }
    }

    public class ProductCategoryVM
    {
        public int ProductCategoryID { get; set; }
        public int ProductMainCategoryID { get; set; }
        public string ProductMainCategoryName { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
