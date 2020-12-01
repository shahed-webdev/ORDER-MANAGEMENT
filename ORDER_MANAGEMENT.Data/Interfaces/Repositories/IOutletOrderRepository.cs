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
        DataResult<OutletOrdered> OrderedDataTable(DataRequest request);
        OutletOrderDelivered OrderDeliveredDetails(int id);
        int OrderDelivered(OutletOrderDelivered model, int RegID);
        ICollection<OutletOrderReportModel> OrderReport(OutletReportFilterModel filterModel);
        ICollection<OutletOrderReportModel> SalesReport(OutletReportFilterModel filterModel);
        ICollection<OutletRevenueReportModelModel> RevenueReport(OutletReportFilterModel filterModel);
        ICollection<OutletOrderVsSalesReportModel> OrderVsSalesReport(OutletReportFilterModel filterModel);
    }
}
