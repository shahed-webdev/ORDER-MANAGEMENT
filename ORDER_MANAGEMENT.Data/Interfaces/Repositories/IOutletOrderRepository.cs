using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IOutletOrderRepository : IRepository<OutletOrder>
    {
        int OrderPlaced(OutletOrderPlace model, int OrderByRegID);
        ICollection<OutletOrdered> OrderHistory(int RegID, int OutletID);
        OutletOrderDetails OrderDetails(int OrderID);
        ICollection<OutletOrdered> UndeliveredOrderList_ByUser(int RegID);
        ICollection<OutletOrdered> OrderedHistory(int RegID);
        OutletOrderDelivered OrderDeliveredDetails(int id);
        int OrderDelivered(OutletOrderDelivered model, int RegID);
    }
}
