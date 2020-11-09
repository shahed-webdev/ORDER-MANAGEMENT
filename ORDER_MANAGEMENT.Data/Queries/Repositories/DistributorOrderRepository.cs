using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorOrderRepository : Repository<DistributorOrder>, IDistributorOrderRepository
    {
        public DistributorOrderRepository(DataContext context) : base(context)
        {

        }

        public ICollection<DistributorOrdered> UnapprovedOrderList()
        {
            var o_List = from o in Context.DistributorOrders
                         where !o.Is_Approved
                         select new DistributorOrdered
                         {
                             DistributorOrderID = o.DistributorOrderID,
                             DistributorOrder_SN = o.DistributorOrder_SN,
                             Is_Approved = o.Is_Approved,
                             DistributorID = o.DistributorID,
                             InsertDate = o.InsertDate,
                             OrderDiscount = o.OrderDiscount,
                             OrderNetPrice = o.OrderNetPrice,
                             OrderTotalPrice = o.OrderTotalPrice,
                             DistributorAddress = o.Distributor.Address,
                             DistributorName = o.Distributor.Name,
                             OrderBy_Name = o.OrderBy_Registration.Name,
                             TerritoryName = o.Distributor.Territory.TerritoryName
                         };
            return o_List.ToList();
        }

        public ICollection<DistributorOrdered> UnapprovedOrderList_ByUser(int RegID)
        {
            var o_List = from o in Context.DistributorOrders
                         where !o.Is_Approved && o.OrderBy_RegistrationID == RegID
                         select new DistributorOrdered
                         {
                             DistributorOrderID = o.DistributorOrderID,
                             DistributorOrder_SN = o.DistributorOrder_SN,
                             Is_Approved = o.Is_Approved,
                             DistributorID = o.DistributorID,
                             InsertDate = o.InsertDate,
                             OrderDiscount = o.OrderDiscount,
                             OrderNetPrice = o.OrderNetPrice,
                             OrderTotalPrice = o.OrderTotalPrice,
                             DistributorAddress = o.Distributor.Address,
                             DistributorName = o.Distributor.Name,
                             OrderBy_Name = o.OrderBy_Registration.Name,
                             TerritoryName = o.Distributor.Territory.TerritoryName
                         };
            return o_List.ToList();
        }
        public int OrderPlaced(DistributorOrderPlace model, int OrderByRegID)
        {
            var order = new DistributorOrder();

            order.OrderBy_RegistrationID = OrderByRegID;
            order.DistributorID = model.DistributorID;
            order.Lat = model.Lat;
            order.Lon = model.Lon;
            order.OrderTotalPrice = model.OrderTotalPrice;
            order.OrderDiscount = model.OrderDiscount;


            var orderList = model.Carts.Select(c => new DistributorOrderList
            {
                OrderQuantity = c.OrderQuantity,
                ProductID = c.ProductID,
                UnitPrice = c.UnitPrice
            }).ToList();

            order.DistributorOrderLists = orderList;
            order.DistributorOrder_SN = this.SN_Count();

            Add(order);

            return order.DistributorOrder_SN;
        }

        public int SN_Count()
        {
            int? conut = Context.DistributorOrders.Max(d => (int?)d.DistributorOrder_SN);

            return conut.GetValueOrDefault() + 1;
        }

        public DistributorOrderApproved OrderApprovedDetails(int id)
        {
            var order = new DistributorOrderApproved();

            order.OrderInfo = (from o in Context.DistributorOrders
                               where o.DistributorOrderID == id
                               select new DistributorOrdered
                               {
                                   DistributorOrderID = o.DistributorOrderID,
                                   DistributorOrder_SN = o.DistributorOrder_SN,
                                   Is_Approved = o.Is_Approved,
                                   DistributorID = o.DistributorID,
                                   InsertDate = o.InsertDate,
                                   OrderDiscount = o.OrderDiscount,
                                   OrderNetPrice = o.OrderNetPrice,
                                   OrderTotalPrice = o.OrderTotalPrice,
                                   DistributorAddress = o.Distributor.Address,
                                   DistributorName = o.Distributor.Name,
                                   OrderBy_Name = o.OrderBy_Registration.Name,
                                   TerritoryName = o.Distributor.Territory.TerritoryName
                               }).FirstOrDefault();

            order.OrderProducts = (from o in Context.DistributorOrderLists
                                   where o.DistributorOrderID == id
                                   select new DistributorProductApproved
                                   {
                                       DistributorOrderListID = o.DistributorOrderListID,
                                       OrderQuantity = o.OrderQuantity,
                                       ProductCode = o.Product.ProductCode,
                                       ProductID = o.ProductID,
                                       ProductName = o.Product.ProductName,
                                       ChangedQuantity = o.OrderQuantity,
                                       UnitPrice = o.UnitPrice
                                   }).ToList();

            return order;
        }

        public void OrderConfirm(DistributorOrderApproved model, int RegID)
        {
            var Dis_OrderChange = model.OrderProducts.Where(o => o.OrderQuantity != o.ChangedQuantity).Select(o =>
            new DistributorOrderChange
            {
                ChangeBy_RegistrationID = RegID,
                ChangedQuantity = o.ChangedQuantity,
                OrderQuantity = o.OrderQuantity,
                DistributorID = model.OrderInfo.DistributorID,
                DistributorOrderID = model.OrderInfo.DistributorOrderID,
                ProductID = o.ProductID,
                Reason = o.Reason
            }).ToList();
            //------------add change 
            Context.DistributorOrderChanges.AddRange(Dis_OrderChange);

            var changed_OrderLists = model.OrderProducts.Where(o => o.OrderQuantity != o.ChangedQuantity).ToList();

            var delete_OrderListIDs = model.OrderProducts.Where(o => o.ChangedQuantity == 0).Select(o => o.DistributorOrderListID).ToList();

            var Unchanged_OrderListIDs = model.OrderProducts.Where(o => o.OrderQuantity == o.ChangedQuantity).Select(o => o.DistributorOrderListID).ToList();

            //---------delete if quantity 0
            var delete_orderlist = Context.DistributorOrderLists.Where(o => delete_OrderListIDs.Contains(o.DistributorOrderListID)).ToList();
            Context.DistributorOrderLists.RemoveRange(delete_orderlist);
            //--------update order list
            foreach (var OrderList in changed_OrderLists)
            {
                var ol = Context.DistributorOrderLists.Find(OrderList.DistributorOrderListID);

                ol.OrderQuantity = OrderList.ChangedQuantity;
                Context.Entry(ol).State = EntityState.Modified;
            }


            var dis_order = Context.DistributorOrders.Find(model.OrderInfo.DistributorOrderID);

            dis_order.Is_Approved = true;
            dis_order.OrderTotalPrice = model.OrderInfo.OrderTotalPrice;
            dis_order.OrderDiscount = model.OrderInfo.OrderDiscount;
            dis_order.ApproveBy_RegistrationID = RegID;
            dis_order.ApproveDate = DateTime.Now;

            Context.Entry(dis_order).State = EntityState.Modified;


            var OrderProductStocks = model.OrderProducts.Where(o => o.ChangedQuantity != 0).ToList();

            foreach (var item in OrderProductStocks)
            {
                var product = Context.DistributorStocks.FirstOrDefault(d => d.DistributorID == model.OrderInfo.DistributorID && d.ProductID == item.ProductID);

                if (product != null)
                {
                    product.Quantity += item.ChangedQuantity;
                    Context.Entry(product).State = EntityState.Modified;
                }
                else
                {
                    product = new DistributorStock
                    {
                        DistributorID = model.OrderInfo.DistributorID,
                        ProductID = item.ProductID,
                        Quantity = item.ChangedQuantity
                    };

                    Context.DistributorStocks.Add(product);
                }
            }


            foreach (var item in OrderProductStocks)
            {
                var product = Context.Products.Find(item.ProductID);
                product.Quantity -= item.ChangedQuantity;
                Context.Entry(product).State = EntityState.Modified;
            }

            //Distributor update
            var Distributor = Context.Distributors.Find(model.OrderInfo.DistributorID);
            Distributor.Total_BuyingAmount += model.OrderInfo.OrderTotalPrice;
            Context.Entry(Distributor).State = EntityState.Modified;

        }

        public void OrderTargetUpdate(int DistributorID, int DistributorOrderID)
        {
            var dis = Context.Distributors.Find(DistributorID);
            dis.Total_BuyingAmount = Context.DistributorOrders.Where(d => d.DistributorID == DistributorID).Sum(d => d.OrderNetPrice);

            Context.Entry(dis).State = EntityState.Modified;

            var OrderNetAmount = Context.DistributorOrders.Find(DistributorOrderID).OrderNetPrice;

            var ta = (from t in Context.User_Territories
                      where t.TerritoryID == dis.TerritoryID
                      select t.User.TargetAssigns).ToList();

            List<int> targetIDs = new List<int>();
            foreach (var item in ta)
            {
                foreach (var i in item)
                {
                    i.AchievedAmount += OrderNetAmount;
                    Context.Entry(i).State = EntityState.Modified;
                    targetIDs.Add(i.TargetID);
                }
            }

            var targets = Context.Targets.Where(t => targetIDs.Contains(t.TargetID)).ToList();

            foreach (var item in targets)
            {
                item.Total_AchievedAmount += OrderNetAmount;
                Context.Entry(item).State = EntityState.Modified;
            }

        }

        public DistributorOrderDetails OrderDetails(int OrderID)
        {
            var order = new DistributorOrderDetails();

            order.OrderInfo = (from o in Context.DistributorOrders
                               where o.DistributorOrderID == OrderID
                               select new DistributorOrdered
                               {
                                   DistributorOrderID = o.DistributorOrderID,
                                   DistributorOrder_SN = o.DistributorOrder_SN,
                                   Is_Approved = o.Is_Approved,
                                   DistributorID = o.DistributorID,
                                   InsertDate = o.InsertDate,
                                   OrderDiscount = o.OrderDiscount,
                                   OrderNetPrice = o.OrderNetPrice,
                                   OrderTotalPrice = o.OrderTotalPrice,
                                   DistributorAddress = o.Distributor.Address,
                                   DistributorName = o.Distributor.Name,
                                   OrderBy_Name = o.OrderBy_Registration.Name,
                                   TerritoryName = o.Distributor.Territory.TerritoryName
                               }).FirstOrDefault();

            order.OrderProducts = (from o in Context.DistributorOrderLists
                                   where o.DistributorOrderID == OrderID
                                   select new DistributorCart
                                   {
                                       DistributorOrderListID = o.DistributorOrderListID,
                                       OrderQuantity = o.NetQuantity,
                                       ProductCode = o.Product.ProductCode,
                                       ProductID = o.ProductID,
                                       ProductName = o.Product.ProductName,
                                       UnitPrice = o.UnitPrice
                                   }).ToList();

            return order;
        }

        public ICollection<DistributorOrdered> OrderHistory(int RegID, int DistributorID)
        {
            var o_List = from o in Context.DistributorOrders
                         where o.OrderBy_RegistrationID == RegID && o.DistributorID == DistributorID
                         select new DistributorOrdered
                         {
                             DistributorOrderID = o.DistributorOrderID,
                             DistributorOrder_SN = o.DistributorOrder_SN,
                             Is_Approved = o.Is_Approved,
                             DistributorID = o.DistributorID,
                             InsertDate = o.InsertDate,
                             OrderDiscount = o.OrderDiscount,
                             OrderNetPrice = o.OrderNetPrice,
                             OrderTotalPrice = o.OrderTotalPrice,
                             DistributorAddress = o.Distributor.Address,
                             DistributorName = o.Distributor.Name,
                             OrderBy_Name = o.OrderBy_Registration.Name,
                             TerritoryName = o.Distributor.Territory.TerritoryName
                         };
            return o_List.ToList();
        }
    }
}
