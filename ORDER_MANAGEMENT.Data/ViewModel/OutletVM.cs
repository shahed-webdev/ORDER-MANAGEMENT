using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletCreateVM
    {
        public int CreateBy_RegistrationID { get; set; }
        public int TerritoryID { get; set; }
        public int DistributorID { get; set; }
        public string OutletName { get; set; }
        public string ProprietorName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public byte[] Logo { get; set; }
    }
    public class OutletListVM
    {
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public string ProprietorName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsApproved { get; set; }
        public int DueRangeLimit { get; set; }
        public double TotalDue { get; set; }
        public byte[] Logo { get; set; }
    }
    public class OutletDueList : OutletListVM
    {
        public double TotalPaid { get; set; }
        public double TotalAmount { get; set; }

    }
    public class OutletOrderPlace
    {
        public OutletOrderPlace()
        {
            this.Carts = new HashSet<OutletCart>();
        }
        public int OutletID { get; set; }
        public int OrderBy_RegistrationID { get; set; }
        public double OrderTotalPrice { get; set; }
        public double OrderDiscount { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public ICollection<OutletCart> Carts { get; set; }
    }
    public class OutletCart
    {
        public int OutletOrderListID { get; set; }
        public int ProductID { get; set; }
        public int OrderQuantity { get; set; }
        public double UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }
    public class OutletOrdered
    {
        public int OutletOrderID { get; set; }
        public int OutletID { get; set; }
        public int OutletOrder_SN { get; set; }
        public string TerritoryName { get; set; }
        public string OrderBy_Name { get; set; }
        public string OutletName { get; set; }
        public string OutletAddress { get; set; }
        public double OrderTotalPrice { get; set; }
        public double OrderDiscount { get; set; }
        public double OrderNetPrice { get; set; }
        public bool Is_Approved { get; set; }
        public DateTime InsertDate { get; set; }

    }
    public class OutletOrderDetails
    {

        public OutletOrderDetails()
        {
            this.OrderProducts = new List<OutletCart>();
        }
        public OutletOrdered OrderInfo { get; set; }
        public List<OutletCart> OrderProducts { get; set; }
    }
    public class OutletProductDelivered
    {
        public int ProductID { get; set; }
        public int OutletOrderListID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int OrderQuantity { get; set; }
        public int ChangedQuantity { get; set; }
        public double UnitPrice { get; set; }
        public string Reason { get; set; }
    }
    public class OutletOrderDelivered
    {
        public OutletOrderDelivered()
        {
            this.OrderProducts = new List<OutletProductDelivered>();
        }
        public OutletOrdered OrderInfo { get; set; }
        public List<OutletProductDelivered> OrderProducts { get; set; }
    }
    public class OutletSearch
    {
        public int OutletID { get; set; }
        public string OutletName { get; set; }
        public string ProprietorName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RegionName { get; set; }
        public string AreaName { get; set; }
        public string TerritoryName { get; set; }
    }

    public class OutletOrderReport
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int OrderQuantity { get; set; }
    }

    public class OutletOrderReportWithFilter
    {
        public int RegionID { get; set; }
        public int AreaID { get; set; }
        public int TerritoryID { get; set; }
        public int OutletID { get; set; }
        public int DistributorID { get; set; }
        public int? DepotId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public DateTime OrderDate { get; set; }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int OrderQuantity { get; set; }

    }

    public class OutletOrderFilter
    {
        public int RegionID { get; set; }
        public int AreaID { get; set; }
        public int[] TerritoryIDs { get; set; }
        public int OutletID { get; set; }
        public int DistributorID { get; set; }
        public int DepotId { get; set; }
        public DateTime? SDateTime { get; set; }
        public DateTime? EDateTime { get; set; }
    }
}
