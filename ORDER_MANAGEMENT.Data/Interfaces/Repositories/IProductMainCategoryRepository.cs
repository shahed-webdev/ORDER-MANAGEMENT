using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IProductMainCategoryRepository : IRepository<ProductMainCategory>
    {
        DataResult<ProductMainCategory> DT(DataRequest request);
        ICollection<DDL> GetDdl();

        ICollection<DDL> GetDdlforSub();
    }
}
