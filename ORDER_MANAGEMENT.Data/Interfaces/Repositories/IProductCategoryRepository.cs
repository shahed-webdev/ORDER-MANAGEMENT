using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        ICollection<DDL> GetProductCategoryDDL();
        ICollection<DDL> GetCategoryByMainDDL(int id);
        DataResult<ProductCategoryVM> DT_CatgoryWise(DataRequest request, int[] filer);

    }
}
