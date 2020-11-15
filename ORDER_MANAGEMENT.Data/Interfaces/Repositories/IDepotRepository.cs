using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IDepotRepository : IRepository<Depot>
    {
        List<DepotViewModel> GetAllDepot();
        DataResult<DepotViewModel> ListDataTable(DataRequest request);
    }


}