using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DataContext context) : base(context)
        {

        }

        public ICollection<DDL> GetProductCategoryDDL()
        {
            return GetAll().Select(p => new DDL { label = p.ProductCategoryName, value = p.ProductCategoryID }).ToList();
        }

        public DataResult<ProductCategoryVM> DT_CatgoryWise(DataRequest request, int[] filer)
        {

            var query = Context.ProductCategories.Include(p => p.ProductMainCategory).Where(t => filer.Contains(t.ProductMainCategoryID)).Select(p => new ProductCategoryVM
            {
                ProductCategoryID = p.ProductCategoryID,
                ProductMainCategoryID = p.ProductMainCategoryID,
                ProductMainCategoryName = p.ProductMainCategory.ProductMainCategoryName,
                ProductCategoryName = p.ProductCategoryName
            });
            return query.ToDataResult(request);
        }

        public ICollection<DDL> GetCategoryByMainDDL(int id)
        {
            return Where(c => c.ProductMainCategoryID == id).Select(p => new DDL { label = p.ProductCategoryName, value = p.ProductCategoryID }).ToList();
        }
    }
}