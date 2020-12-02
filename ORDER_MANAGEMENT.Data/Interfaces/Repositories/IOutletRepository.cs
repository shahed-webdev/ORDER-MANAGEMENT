using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IOutletRepository : IRepository<Outlet>
    {
        bool IsExist(string number);
        void CreateOutlet(OutletCreateVM model);
        DbResponse DeleteOutlet(int outletId);
        DbResponse UpdateDetails(OutletDetailsUpdateModel model);
        List<OutletListVM> OutletList();
        List<OutletListVM> OutletListByUser(int registrationId);
        double GetDueRange(int OutletID);
        void DueRangeChange(int OutletID, int DueRange);
        void Approved(int OutletID, int RegistrationID, int DueRange);
        List<DDL> OutletByUserTerritory(int RegistrationID);
        List<DDL> OutletByTerritorys(List<int> TerritoryIDs);
        ICollection<OutletSearch> Search(string key);
        ICollection<OutletSearch> Search(string key, int currentOutletId);
    }


}
