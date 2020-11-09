using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IDistributorProductReturnRepository : IRepository<DistributorProductReturn>
    {
        ICollection<DistributorOrdered> OrderedHistory(int RegID);

        ICollection<DistributorOrdered> ApprovedReturnPendingList();
        void ApprovedReturn(int id, int RegID, double ReturnAmount);
        void RejectReturn(int id);
        DistributorOrderReturnDetails ApprovedReturnOrderDetails(int OrderID);
    }
}
