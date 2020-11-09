using System;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderChange
    {
        public int OutletOrderChangeID { get; set; }
        public int OutletOrderID { get; set; }
        public int OutletID { get; set; }
        public int ChangeBy_RegistrationID { get; set; }
        public int ProductID { get; set; }
        public int OrderQuantity { get; set; }
        public int ChangedQuantity { get; set; }
        public string Reason { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Outlet Outlet { get; set; }
        public virtual Product Product { get; set; }
        public virtual OutletOrder OutletOrder { get; set; }
    }
}









