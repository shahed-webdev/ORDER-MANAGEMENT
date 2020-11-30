using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace ORDER_MANAGEMENT.Data
{
    public class DistributorProductReturnRepository : Repository<DistributorProductReturn>, IDistributorProductReturnRepository
    {
        public DistributorProductReturnRepository(DataContext context) : base(context)
        {

        }

        public void ApprovedReturn(int id, int RegID, double ReturnAmount)
        {
            var pr = Find(id);

            pr.Is_Approved = true;
            pr.ApproveBy_RegistrationID = RegID;
            pr.ApproveDate = DateTime.Now;
            Update(pr);

            var ol = Context.DistributorOrderLists.Find(pr.DistributorOrderListID);

            ol.ReturnQuantity = ol.ReturnQuantity + pr.ReturnQuantity;
            Context.Entry(ol).State = EntityState.Modified;


            var order = Context.DistributorOrders.Find(pr.DistributorOrderID);
            order.OrderReturnPrice = order.OrderReturnPrice + ReturnAmount;
            Context.Entry(order).State = EntityState.Modified;

            //Distributor update
            var Distributor = Context.Distributors.Find(pr.DistributorID);
            Distributor.Total_ReturnAmount += ReturnAmount;
            Context.Entry(Distributor).State = EntityState.Modified;

        }
        public void RejectReturn(int id)
        {
            var pr = Find(id);

            Remove(pr);
        }
        public DistributorOrderReturnDetails ApprovedReturnOrderDetails(int OrderID)
        {

            var order = new DistributorOrderReturnDetails();

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
                                   TerritoryName = o.Distributor.DistributorTerritoryLists.Select(t => t.Territory.TerritoryName).FirstOrDefault()
                               }).FirstOrDefault();

            order.OrderReturnItems = (from o in Context.DistributorOrderLists
                                      join r in Context.DistributorProductReturns
                            on o.DistributorOrderListID equals r.DistributorOrderListID
                                      where o.DistributorOrderID == OrderID && !r.Is_Approved
                                      select new DistributorOrderReturnItem
                                      {
                                          DistributorProductReturnID = r.DistributorProductReturnID,
                                          ReturnAmount = (double)r.ReturnQuantity * o.UnitPrice,
                                          ReturnQuantity = r.ReturnQuantity,
                                          DistributorOrderListID = o.DistributorOrderListID,
                                          OrderQuantity = o.NetQuantity,
                                          ProductCode = o.Product.ProductCode,
                                          ProductID = o.ProductID,
                                          ProductName = o.Product.ProductName,
                                          UnitPrice = o.UnitPrice
                                      }).ToList();

            return order;
        }

        public ICollection<DistributorOrdered> ApprovedReturnPendingList()
        {
            var o_List = from
                         o in Context.DistributorProductReturns
                         where !o.Is_Approved
                         select new DistributorOrdered
                         {
                             DistributorOrderID = o.DistributorOrderID,
                             DistributorOrder_SN = o.DistributorOrder.DistributorOrder_SN,
                             Is_Approved = o.DistributorOrder.Is_Approved,
                             DistributorID = o.DistributorID,
                             InsertDate = o.DistributorOrder.InsertDate,
                             OrderDiscount = o.DistributorOrder.OrderDiscount,
                             OrderNetPrice = o.DistributorOrder.OrderNetPrice,
                             OrderTotalPrice = o.DistributorOrder.OrderTotalPrice,
                             DistributorAddress = o.Distributor.Address,
                             DistributorName = o.Distributor.Name,
                             OrderBy_Name = o.DistributorOrder.OrderBy_Registration.Name,
                             TerritoryName = o.Distributor.DistributorTerritoryLists.Select(t => t.Territory.TerritoryName).FirstOrDefault()
                         };
            return o_List.Distinct().ToList();
        }

        public ICollection<DistributorOrdered> OrderedHistory(int RegID)
        {
            var o_List = from o in Context.DistributorOrders
                         where o.OrderBy_RegistrationID == RegID && o.Is_Approved
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
                             TerritoryName = o.Distributor.DistributorTerritoryLists.Select(t => t.Territory.TerritoryName).FirstOrDefault()
                         };
            return o_List.ToList();
        }


    }
}
