using System;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorOrderChange
    {
        public int DistributorOrderChangeID { get; set; }
        public int DistributorOrderID { get; set; }
        public int DistributorID { get; set; }
        public int ChangeBy_RegistrationID { get; set; }
        public int ProductID { get; set; }
        public int OrderQuantity { get; set; }
        public int ChangedQuantity { get; set; }
        public string Reason { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Distributor Distributor { get; set; }
        public virtual Product Product { get; set; }
        public virtual DistributorOrder DistributorOrder { get; set; }
        public virtual Registration ChangeBy_Registration { get; set; }
    }
}
