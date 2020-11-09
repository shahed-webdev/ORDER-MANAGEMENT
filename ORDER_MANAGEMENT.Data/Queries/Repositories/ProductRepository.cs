using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace ORDER_MANAGEMENT.Data
{
    class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {

        }

        public void AddQuantity(int ProductID, int RegistrationID, int Quantity)
        {
            var PQ_record = new ProductQuantityRecord();
            PQ_record.InsertByRegistrationID = RegistrationID;
            PQ_record.ProductID = ProductID;
            PQ_record.Quantity = Quantity;
            Context.ProductQuantityRecords.Add(PQ_record);
        }

        public ICollection<Product> GetCategoryProducts(List<int> ProductCategoryIDs)
        {
            var Products = new List<Product>();
            Products = GetAll().ToList();

            return Products.Where(p => ProductCategoryIDs.Contains(p.ProductCategoryID)).ToList();
        }

        public DataResult<Product> DT_GetCategoryProducts(DataRequest request, int[] filer)
        {
            var query = Context.Products.Where(t => filer.Contains(t.ProductCategoryID));
            return query.ToDataResult(request);
        }

        public ICollection<ProductVM> GetProductBySearch(string key)
        {
            key = key.ToLower();
            if (!string.IsNullOrEmpty(key))
            {
                var Products = GetAll().Where(p => p.IsActive && (p.ProductCode.ToLower().Contains(key) || p.ProductName.ToLower().Contains(key))).Select(p => new ProductVM
                {
                    Description = p.Description,
                    MRP = p.MRP,
                    ProductCategoryID = p.ProductCategoryID,
                    ProductCode = p.ProductCode,
                    ProductID = p.ProductID,
                    ProductImage = p.ProductImage,
                    ProductName = p.ProductName,
                    Size = p.Size,
                    SKU = p.SKU
                }).Take(20);

                return Products.ToList();
            }
            else
            {
                var Products = GetAll().Where(p => p.IsActive).Select(p => new ProductVM
                {
                    Description = p.Description,
                    MRP = p.MRP,
                    ProductCategoryID = p.ProductCategoryID,
                    ProductCode = p.ProductCode,
                    ProductID = p.ProductID,
                    ProductImage = p.ProductImage,
                    ProductName = p.ProductName,
                    Size = p.Size,
                    SKU = p.SKU
                }).Take(20);

                return Products.ToList();

            }


        }

        public ICollection<ProductVM> GetProductWithDistributorPrice(int DistributorID, string key)
        {
            key = key.ToLower();

            var DistributorDiscountPercentage = Context.Distributors.Include(d => d.Territory).FirstOrDefault(d => d.DistributorID == DistributorID).Territory.DistributorDiscountPercentage;


            if (!string.IsNullOrEmpty(key))
            {
                var Products = GetAll().Where(p => p.IsActive && (p.ProductCode.ToLower().Contains(key) || p.ProductName.ToLower().Contains(key))).Select(p => new ProductVM
                {
                    Description = p.Description,
                    MRP = p.MRP - p.MRP * (DistributorDiscountPercentage / 100),
                    ProductCategoryID = p.ProductCategoryID,
                    ProductCode = p.ProductCode,
                    ProductID = p.ProductID,
                    ProductImage = p.ProductImage,
                    ProductName = p.ProductName,
                    Size = p.Size,
                    SKU = p.SKU
                }).Take(20);

                return Products.ToList();
            }
            else
            {
                var Products = GetAll().Where(p => p.IsActive).Select(p => new ProductVM
                {
                    Description = p.Description,
                    MRP = p.MRP - p.MRP * (DistributorDiscountPercentage / 100),
                    ProductCategoryID = p.ProductCategoryID,
                    ProductCode = p.ProductCode,
                    ProductID = p.ProductID,
                    ProductImage = p.ProductImage,
                    ProductName = p.ProductName,
                    Size = p.Size,
                    SKU = p.SKU
                }).Take(20);

                return Products.ToList();

            }
        }

        public ICollection<Product> LowStock(int limit)
        {
            return Where(p => p.Quantity <= limit).ToList();
        }
    }
}
