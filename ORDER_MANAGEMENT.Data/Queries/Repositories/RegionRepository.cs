using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        public RegionRepository(DataContext context) : base(context)
        {

        }
        public List<RegionDll_VM> GetDllRegion()
        {
            var regions = new List<Region>();
            regions = GetAll().ToList();

            return regions.Select(r => new RegionDll_VM() { RegionID = r.RegionID, RegionName = r.RegionName }).ToList();
        }
        public List<RegionDll_VM> GetUserRegion(int RegistrationID)
        {
            var user = Context.Users.Find(RegistrationID);
            var regions = new List<Region>();

            if (user.IsDefaultUser)
            {
                regions = GetAll().ToList();
            }
            else
            {
                regions = Context.User_Territories.Where(u => u.RegistrationID == RegistrationID).Select(t => t.Territory.Area.Region).Distinct().ToList();
            }
            return regions.Select(r => new RegionDll_VM() { RegionID = r.RegionID, RegionName = r.RegionName }).ToList();
        }
    }
}