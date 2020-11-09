using System;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletProductReturn
    {
        public int OutletProductReturnID { get; set; }
        public int OutletOrderListID { get; set; }
        public int OutletID { get; set; }
        public int OutletOrderID { get; set; }
        public int ProductID { get; set; }
        public int ReturnQuantity { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ReturnBy_RegistrationID { get; set; }
        public int ApproveBy_RegistrationID { get; set; }
        public DateTime ApproveDate { get; set; }
        public bool Is_Approved { get; set; } = false;
        public virtual Outlet Outlet { get; set; }
        public virtual Product Product { get; set; }
        public virtual OutletOrder OutletOrder { get; set; }
        public virtual OutletOrderList OutletOrderList { get; set; }

    }
}

