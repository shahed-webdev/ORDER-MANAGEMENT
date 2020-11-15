using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotRepository : Repository<Depot>, IDepotRepository
    {
        public DepotRepository(DataContext context) : base(context)
        {
        }

        public List<DepotViewModel> GetAllDepot()
        {
            var query = Context.Depots.Select(d => new DepotViewModel
            {
                DepotId = d.DepotId,
                DepotName = d.DepotName,
                RegionName = d.Region.RegionName,
                Incharge = d.User.Registration.Name
            });
            return query.ToList();
        }

        public DataResult<DepotViewModel> ListDataTable(DataRequest request)
        {
            var query = Context.Depots.Select(d => new DepotViewModel
            {
                DepotId = d.DepotId,
                DepotName = d.DepotName,
                RegionName = d.Region.RegionName,
                Incharge = d.User.Registration.Name
            });

            return query.ToDataResult(request);
        }
    }
}