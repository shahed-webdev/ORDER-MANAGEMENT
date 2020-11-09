using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(DataContext context) : base(context)
        {

        }
        public List<Area> GetAllArea()
        {
            var Areas = new List<Area>();
            Areas = GetAll().ToList();

            return Areas;
        }
        public List<Area> GetAreaByRegion(List<int> RegionIDs)
        {
            var Areas = new List<Area>();
            Areas = Where(a => RegionIDs.Contains(a.RegionID)).ToList();

            return Areas;
        }
        public List<AreaDll_VM> GetDll_AreaByRegion(List<int> RegionIDs)
        {
            var Areas = new List<Area>();
            Areas = GetAll().ToList();

            return Areas.Where(a => RegionIDs.Contains(a.RegionID)).Select(a => new AreaDll_VM() { AreaID = a.AreaID, AreaName = a.AreaName }).ToList();
        }
        public List<AreaDll_VM> GetUserArea(int RegistrationID)
        {
            var user = Context.Users.Find(RegistrationID);
            var Areas = new List<Area>();

            if (user.IsDefaultUser)
            {
                Areas = GetAll().ToList();
            }
            else
            {
                Areas = Context.User_Territories.Where(u => u.RegistrationID == RegistrationID).Select(t => t.Territory.Area).Distinct().ToList();
            }
            return Areas.Select(a => new AreaDll_VM() { AreaID = a.AreaID, AreaName = a.AreaName }).ToList();
        }
        public List<AreaDll_VM> GetUserArea(int RegistrationID, List<int> RegionIDs)
        {
            var user = Context.Users.Find(RegistrationID);
            var Areas = new List<Area>();

            if (user.IsDefaultUser)
            {
                Areas = GetAll().ToList();
            }
            else
            {
                Areas = Context.User_Territories.Where(u => u.RegistrationID == RegistrationID).Select(t => t.Territory.Area).Distinct().ToList();
            }
            return Areas.Where(a => RegionIDs.Contains(a.RegionID)).Select(a => new AreaDll_VM() { AreaID = a.AreaID, AreaName = a.AreaName }).ToList();
        }
    }
}