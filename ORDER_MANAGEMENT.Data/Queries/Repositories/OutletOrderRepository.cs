using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderRepository : Repository<OutletOrder>, IOutletOrderRepository
    {
        public OutletOrderRepository(DataContext context) : base(context)
        {

        }

        public int OrderPlaced(OutletOrderPlace model, int OrderByRegID)
        {
            var DistributorID = Context.Outlets.Find(model.OutletID).DistributorID;
            //Context.Distributors.FirstOrDefault(d => d.ReportTo_RegistrationID == OrderByRegID).DistributorID;
            var order = new OutletOrder();

            order.OrderBy_RegistrationID = OrderByRegID;
            order.DistributorID = DistributorID;
            order.OutletID = model.OutletID;
            order.Lat = model.Lat;
            order.Lon = model.Lon;
            order.OrderTotalPrice = model.OrderTotalPrice;
            order.OrderDiscount = model.OrderDiscount;


            var orderList = model.Carts.Select(c => new OutletOrderList
            {
                OrderQuantity = c.OrderQuantity,
                ProductID = c.ProductID,
                UnitPrice = c.UnitPrice
            }).ToList();

            order.OutletOrderLists = orderList;
            order.OutletOrder_SN = this.SN_Count();

            Add(order);

            return order.OutletOrder_SN;
        }

        public int SN_Count()
        {
            int? conut = Context.OutletOrders.Max(d => (int?)d.OutletOrder_SN);

            return conut.GetValueOrDefault() + 1;
        }
        public ICollection<OutletOrdered> OrderHistory(int RegID, int OutletID)
        {
            var o_List = (from o in Context.OutletOrders
                          where o.OrderBy_RegistrationID == RegID && o.OutletID == OutletID
                          select new OutletOrdered
                          {
                              OutletOrderID = o.OutletOrderID,
                              OutletOrder_SN = o.OutletOrder_SN,
                              Is_Approved = o.Is_Approved,
                              OutletID = o.OutletID,
                              InsertDate = o.InsertDate,
                              OrderDiscount = o.OrderDiscount,
                              OrderNetPrice = o.OrderNetPrice,
                              OrderTotalPrice = o.OrderTotalPrice,
                              OutletAddress = o.Outlet.Address,
                              OutletName = o.Outlet.OutletName,
                              OrderBy_Name = o.OrderBy_Registration.Name,
                              TerritoryName = o.Outlet.Territory.TerritoryName
                          }).ToList();
            return o_List;
        }
        public OutletOrderDetails OrderDetails(int OrderID)
        {
            var order = new OutletOrderDetails();

            order.OrderInfo = (from o in Context.OutletOrders
                               where o.OutletOrderID == OrderID
                               select new OutletOrdered
                               {
                                   OutletOrderID = o.OutletOrderID,
                                   OutletOrder_SN = o.OutletOrder_SN,
                                   Is_Approved = o.Is_Approved,
                                   OutletID = o.OutletID,
                                   InsertDate = o.InsertDate,
                                   OrderDiscount = o.OrderDiscount,
                                   OrderNetPrice = o.OrderNetPrice,
                                   OrderTotalPrice = o.OrderTotalPrice,
                                   OutletAddress = o.Outlet.Address,
                                   OutletName = o.Outlet.OutletName,
                                   OrderBy_Name = o.OrderBy_Registration.Name,
                                   TerritoryName = o.Outlet.Territory.TerritoryName
                               }).FirstOrDefault();

            order.OrderProducts = (from o in Context.OutletOrderLists
                                   where o.OutletOrderID == OrderID
                                   select new OutletCart
                                   {
                                       OutletOrderListID = o.OutletOrderListID,
                                       OrderQuantity = o.NetQuantity,
                                       ProductCode = o.Product.ProductCode,
                                       ProductID = o.ProductID,
                                       ProductName = o.Product.ProductName,
                                       UnitPrice = o.UnitPrice
                                   }).ToList();

            return order;
        }
        public ICollection<OutletOrdered> UndeliveredOrderList_ByUser(int RegID)
        {
            var o_List = from o in Context.OutletOrders
                         where !o.Is_Approved && o.OrderBy_RegistrationID == RegID
                         select new OutletOrdered
                         {
                             OutletOrderID = o.OutletOrderID,
                             OutletOrder_SN = o.OutletOrder_SN,
                             Is_Approved = o.Is_Approved,
                             OutletID = o.OutletID,
                             InsertDate = o.InsertDate,
                             OrderDiscount = o.OrderDiscount,
                             OrderNetPrice = o.OrderNetPrice,
                             OrderTotalPrice = o.OrderTotalPrice,
                             OutletAddress = o.Outlet.Address,
                             OutletName = o.Outlet.OutletName,
                             OrderBy_Name = o.OrderBy_Registration.Name,
                             TerritoryName = o.Outlet.Territory.TerritoryName
                         };
            return o_List.ToList();
        }


        public OutletOrderDelivered OrderDeliveredDetails(int id)
        {
            var order = new OutletOrderDelivered();

            order.OrderInfo = (from o in Context.OutletOrders
                               where o.OutletOrderID == id
                               select new OutletOrdered
                               {
                                   OutletOrderID = o.OutletOrderID,
                                   OutletOrder_SN = o.OutletOrder_SN,
                                   Is_Approved = o.Is_Approved,
                                   OutletID = o.OutletID,
                                   InsertDate = o.InsertDate,
                                   OrderDiscount = o.OrderDiscount,
                                   OrderNetPrice = o.OrderNetPrice,
                                   OrderTotalPrice = o.OrderTotalPrice,
                                   OutletAddress = o.Outlet.Address,
                                   OutletName = o.Outlet.OutletName,
                                   OrderBy_Name = o.OrderBy_Registration.Name,
                                   TerritoryName = o.Outlet.Territory.TerritoryName
                               }).FirstOrDefault();

            order.OrderProducts = (from o in Context.OutletOrderLists
                                   where o.OutletOrderID == id
                                   select new OutletProductDelivered
                                   {
                                       OutletOrderListID = o.OutletOrderListID,
                                       OrderQuantity = o.OrderQuantity,
                                       ProductCode = o.Product.ProductCode,
                                       ProductID = o.ProductID,
                                       ProductName = o.Product.ProductName,
                                       ChangedQuantity = o.OrderQuantity,
                                       UnitPrice = o.UnitPrice
                                   }).ToList();

            return order;
        }


        public int OrderDelivered(OutletOrderDelivered model, int RegID)
        {
            var Outlet_order = Context.OutletOrders.Find(model.OrderInfo.OutletOrderID);

            var DistributorID = Outlet_order.DistributorID;
            var OrderProductStocks = model.OrderProducts.Where(o => o.ChangedQuantity != 0).ToList();
            bool Is_Product_Stockout = false;
            foreach (var item in OrderProductStocks)
            {
                var product = Context.DistributorStocks.FirstOrDefault(d => d.DistributorID == DistributorID && d.ProductID == item.ProductID && d.Quantity >= item.ChangedQuantity);

                if (product == null)
                {
                    Is_Product_Stockout = true;
                }
            }

            if (Is_Product_Stockout) return -1;

            var Outlet_OrderChange = model.OrderProducts
                .Where(o => o.OrderQuantity != o.ChangedQuantity).Select(o =>
                  new OutletOrderChange
                  {
                      ChangeBy_RegistrationID = RegID,
                      ChangedQuantity = o.ChangedQuantity,
                      OrderQuantity = o.OrderQuantity,
                      OutletID = model.OrderInfo.OutletID,
                      OutletOrderID = model.OrderInfo.OutletOrderID,
                      ProductID = o.ProductID,
                      Reason = o.Reason
                  }).ToList();
            //------------add change 
            Context.OutletOrderChanges.AddRange(Outlet_OrderChange);

            var changed_OrderLists = model.OrderProducts.Where(o => o.OrderQuantity != o.ChangedQuantity).ToList();

            var delete_OrderListIDs = model.OrderProducts.Where(o => o.ChangedQuantity == 0).Select(o => o.OutletOrderListID).ToList();

            var Unchanged_OrderListIDs = model.OrderProducts.Where(o => o.OrderQuantity == o.ChangedQuantity).Select(o => o.OutletOrderListID).ToList();

            //---------delete if quantity 0
            var delete_orderlist = Context.OutletOrderLists.Where(o => delete_OrderListIDs.Contains(o.OutletOrderListID)).ToList();
            Context.OutletOrderLists.RemoveRange(delete_orderlist);
            //--------update order list
            foreach (var OrderList in changed_OrderLists)
            {
                var ol = Context.OutletOrderLists.Find(OrderList.OutletOrderListID);

                ol.OrderQuantity = OrderList.ChangedQuantity;
                Context.Entry(ol).State = EntityState.Modified;
            }




            Outlet_order.Is_Approved = true;
            Outlet_order.OrderTotalPrice = model.OrderInfo.OrderTotalPrice;
            Outlet_order.OrderDiscount = model.OrderInfo.OrderDiscount;
            Outlet_order.ApproveBy_RegistrationID = RegID;
            Outlet_order.ApproveDate = DateTime.Now;

            Context.Entry(Outlet_order).State = EntityState.Modified;


            foreach (var item in OrderProductStocks)
            {
                var product = Context.DistributorStocks.FirstOrDefault(d => d.DistributorID == DistributorID && d.ProductID == item.ProductID);

                if (product != null)
                {
                    product.Quantity -= item.ChangedQuantity;
                    Context.Entry(product).State = EntityState.Modified;
                }
            }


            //Distributor update
            var Distributor = Context.Distributors.Find(DistributorID);
            Distributor.Total_SellingAmount += model.OrderInfo.OrderNetPrice;
            Context.Entry(Distributor).State = EntityState.Modified;

            //Outlet update
            var Outlet = Context.Outlets.Find(model.OrderInfo.OutletID);
            Outlet.Total_BuyingAmount += model.OrderInfo.OrderNetPrice;
            Context.Entry(Outlet).State = EntityState.Modified;

            return 0;
        }
        public ICollection<OutletOrdered> OrderedHistory(int RegID)
        {
            var o_List = from o in Context.OutletOrders
                         where o.OrderBy_RegistrationID == RegID && o.Is_Approved
                         select new OutletOrdered
                         {
                             OutletOrderID = o.OutletOrderID,
                             OutletOrder_SN = o.OutletOrder_SN,
                             Is_Approved = o.Is_Approved,
                             OutletID = o.OutletID,
                             InsertDate = o.InsertDate,
                             OrderDiscount = o.OrderDiscount,
                             OrderNetPrice = o.OrderNetPrice,
                             OrderTotalPrice = o.OrderTotalPrice,
                             OutletAddress = o.Outlet.Address,
                             OutletName = o.Outlet.OutletName,
                             OrderBy_Name = o.OrderBy_Registration.Name,
                             TerritoryName = o.Outlet.Territory.TerritoryName
                         };
            return o_List.ToList();
        }


    }
}
