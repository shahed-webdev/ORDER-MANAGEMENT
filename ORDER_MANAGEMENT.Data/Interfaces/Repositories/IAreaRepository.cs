using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IAreaRepository : IRepository<Area>
    {
        List<Area> GetAllArea();
        List<AreaDll_VM> GetDll_AreaByRegion(List<int> RegionIDs);
        List<Area> GetAreaByRegion(List<int> RegionIDs);
        List<AreaDll_VM> GetUserArea(int RegistrationID);
        List<AreaDll_VM> GetUserArea(int RegistrationID, List<int> RegionIDs);
    }
}
