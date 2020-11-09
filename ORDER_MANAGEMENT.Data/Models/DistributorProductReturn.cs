using System;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorProductReturn
    {
        public int DistributorProductReturnID { get; set; }
        public int DistributorOrderListID { get; set; }
        public int DistributorID { get; set; }
        public int DistributorOrderID { get; set; }
        public int ProductID { get; set; }
        public int ReturnQuantity { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ReturnBy_RegistrationID { get; set; }
        public int? ApproveBy_RegistrationID { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool Is_Approved { get; set; } = false;
        public virtual Distributor Distributor { get; set; }
        public virtual Product Product { get; set; }
        public virtual DistributorOrder DistributorOrder { get; set; }
        public virtual DistributorOrderList DistributorOrderList { get; set; }
        public virtual Registration ReturnBy_Registration { get; set; }
        public virtual Registration ApproveBy_Registration { get; set; }
    }
}
