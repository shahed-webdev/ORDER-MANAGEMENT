using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IDistributorRepository : IRepository<Distributor>
    {
        void CreateDistributor(DistributorCreateVM model);

        DbResponse DeleteDistributor(int distributorId);

        bool IsExist(string number);
        List<DistributorListVM> DistributorListByUser(int id);

        List<DistributorListWithUserVM> DistributorListWithUser();
        List<DistributorListVM> DistributorList();
        double GetDueRange(int DistributorID);
        void Approved(int ApproveBy_RegistrationID, DistirubtorRegisterVM Reg, Registration r);

        int DistributerCount(DistributorTypes Types);
        List<DDL> DistributorByUserTerritory(int RegistrationID);
        List<DDL> DistributorByTerritorys(ICollection<int> TerritoryIDs);

        DistributorDetails GetDetails(int? DistributorID);

        void UpdateDetails(DistributorDetails model);

        void AssignSR(int DistributorID, ICollection<int> SR_IDs);

        void AssignDepot(int distributorId, int depotId);


    }
}
