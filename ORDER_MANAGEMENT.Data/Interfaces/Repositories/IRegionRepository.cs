using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IRegionRepository : IRepository<Region>
    {
        List<RegionDll_VM> GetDllRegion();
        List<RegionDll_VM> GetUserRegion(int RegistrationID);
    }
}
