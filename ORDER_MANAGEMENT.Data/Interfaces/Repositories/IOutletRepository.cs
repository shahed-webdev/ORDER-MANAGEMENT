using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IOutletRepository : IRepository<Outlet>
    {
        void CreateOutlet(OutletCreateVM model);
        List<OutletListVM> OutletList();
        double GetDueRange(int OutletID);
        void Approved(int OutletID, int RegistrationID, int DueRange);
        List<DDL> OutletByUserTerritory(int RegistrationID);
        ICollection<OutletSearch> Search(string key);
        ICollection<OutletSearch> Search(string key, int currentOutletId);
    }
}
