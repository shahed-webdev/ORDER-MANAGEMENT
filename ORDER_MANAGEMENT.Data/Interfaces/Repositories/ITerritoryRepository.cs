using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface ITerritoryRepository : IRepository<Territory>
    {
        List<Territory> GetTerritory(List<int> AreaIDs);
        List<DDL> GetUserTerritory(int RegistrationID);
        List<DDL> GetDistributorTerritory(int distributorId);

        List<TerritoryDll_VM> GetUserTerritory(int UpperRegistrationID, List<int> AreaIDs);
        List<TerritoryDll_VM> GetUserUnAsignTerritory(int UpperRegistrationID, List<int> AreaIDs, int RegistrationID = 0);
        void DistributorPercentageChange(int TerritoryID, double Percentage);
    }
}
