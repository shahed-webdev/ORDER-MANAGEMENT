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
                RegionID =d.RegionID,
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
                RegionID = d.RegionID,
                DepotName = d.DepotName,
                RegionName = d.Region.RegionName,
                Incharge = d.User.Registration.Name
            });

            return query.ToDataResult(request);
        }

        public List<DDL> Ddls(int regionId = 0)
        {
            var query = Context.Depots
                .Where(d => d.RegionID == regionId || regionId == 0)
                .Select(d => new DDL
                {
                    value = d.DepotId,
                    label = d.DepotName
                }).ToList();

            return query;

        }
    }
}