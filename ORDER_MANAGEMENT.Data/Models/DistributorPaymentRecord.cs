using System;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorPaymentRecord
    {
        public int DistributorPaymentID { get; set; }
        public int DistributorID { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public virtual Distributor Distributor { get; set; }
    }
}
