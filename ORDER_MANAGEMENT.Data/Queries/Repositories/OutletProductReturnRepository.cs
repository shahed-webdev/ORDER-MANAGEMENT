using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletProductReturnRepository : Repository<OutletProductReturn>, IOutletProductReturnRepository
    {
        public OutletProductReturnRepository(DataContext context) : base(context)
        {

        }

        public void ApprovedReturn(List<OutletProductReturn> model, int RegID)
        {
            double ReturnAmount = 0;
            int OutletOrderID = 0;
            int OutletID = 0;
            foreach (var item in model)
            {
                OutletOrderID = item.OutletOrderID;
                OutletID = item.OutletID;

                item.ReturnBy_RegistrationID = RegID;
                item.ReturnDate = DateTime.Now;
                item.Is_Approved = true;
                item.ApproveBy_RegistrationID = RegID;
                item.ApproveDate = DateTime.Now;



                var ol = Context.OutletOrderLists.Find(item.OutletOrderListID);

                ol.ReturnQuantity = ol.ReturnQuantity + item.ReturnQuantity;
                Context.Entry(ol).State = EntityState.Modified;

                ReturnAmount += ol.UnitPrice * item.ReturnQuantity;
            }
            Context.OutletProductReturns.AddRange(model);


            var order = Context.OutletOrders.Find(OutletOrderID);
            order.OrderReturnPrice = order.OrderReturnPrice + ReturnAmount;
            Context.Entry(order).State = EntityState.Modified;


            //Outlet update
            var Outlet = Context.Outlets.Find(OutletID);
            Outlet.Total_ReturnAmount += ReturnAmount;
            Context.Entry(Outlet).State = EntityState.Modified;
        }
    }
}
