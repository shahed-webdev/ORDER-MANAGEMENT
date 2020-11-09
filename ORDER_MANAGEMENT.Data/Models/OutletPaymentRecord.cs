using System;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletPaymentRecord
    {
        public int OutletPaymentID { get; set; }
        public int OutletID { get; set; }
        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public virtual Outlet Outlet { get; set; }
    }
}