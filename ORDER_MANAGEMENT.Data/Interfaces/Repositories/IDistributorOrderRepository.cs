using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IDistributorOrderRepository : IRepository<DistributorOrder>
    {
        int SN_Count();

        int OrderPlaced(DistributorOrderPlace model, int OrderByRegID);

        void OrderConfirm(DistributorOrderApproved model, int RegID);
        void OrderTargetUpdate(int DistributorID, int DistributorOrderID);

        ICollection<DistributorOrdered> UnapprovedOrderList();
        ICollection<DistributorOrdered> UnapprovedOrderList_ByUser(int RegID);
        ICollection<DistributorOrdered> OrderHistory(int RegID, int DistributorID);

        DistributorOrderApproved OrderApprovedDetails(int OrderID);
        DistributorOrderDetails OrderDetails(int OrderID);
    }
}
