using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        void AddQuantity(int ProductID, int RegistrationID, int Quantity);
        ICollection<Product> LowStock(int limit);
        ICollection<Product> GetCategoryProducts(List<int> ProductCategoryIDs);
        DataResult<Product> DT_GetCategoryProducts(DataRequest request, int[] filer);
        //ICollection<Product> TopSelling();

        ICollection<ProductVM> GetProductBySearch(string key);

        ICollection<ProductVM> GetProductWithDistributorPrice(int DistributorID, string key);

    }
}
