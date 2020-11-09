using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductMainCategoryRepository : Repository<ProductMainCategory>, IProductMainCategoryRepository
    {
        public ProductMainCategoryRepository(DataContext context) : base(context)
        {

        }

        public DataResult<ProductMainCategory> DT(DataRequest request)
        {
            return Context.ProductMainCategories.ToDataResult(request);
        }

        public ICollection<DDL> GetDdl()
        {
            return GetAll().Select(p => new DDL { label = p.ProductMainCategoryName, value = p.ProductMainCategoryID }).ToList();
        }

        public ICollection<DDL> GetDdlforSub()
        {
            return Context.ProductMainCategories.Where(c => c.ProductCategories.Any()).Select(p => new DDL { label = p.ProductMainCategoryName, value = p.ProductMainCategoryID }).ToList();
        }
    }
}
