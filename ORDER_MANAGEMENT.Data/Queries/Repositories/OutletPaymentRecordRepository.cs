using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace ORDER_MANAGEMENT.Data
{
    public class OutletPaymentRecordRepository : Repository<OutletPaymentRecord>, IOutletPaymentRecordRepository
    {
        public OutletPaymentRecordRepository(DataContext context) : base(context)
        {

        }

        public List<OutletDueList> OutletDueList()
        {
            var Outlets = from o in Context.Outlets
                          where o.Total_DueAmount > 0
                          select new OutletDueList()
                          {
                              Address = o.Address,
                              ProprietorName = o.ProprietorName,
                              DueRangeLimit = o.DueRangeLimit,
                              InsertDate = o.InsertDate,
                              Lat = o.Lat,
                              Lon = o.Lon,
                              Phone = o.Phone,
                              OutletName = o.OutletName,
                              TotalDue = o.Total_DueAmount,
                              Logo = o.Logo,
                              OutletID = o.OutletID,
                              IsApproved = o.IsApproved,
                              TotalAmount = o.Total_BuyingAmount - o.Total_ReturnAmount,
                              TotalPaid = o.Total_PaidAmount
                          };
            return Outlets.ToList();
        }

        public void PayDue(OutletPaymentRecord model)
        {
            Add(model);

            //Outlet update
            var Outlet = Context.Outlets.Find(model.OutletID);
            Outlet.Total_PaidAmount += model.Amount;
            Context.Entry(Outlet).State = EntityState.Modified;
        }
    }
}
