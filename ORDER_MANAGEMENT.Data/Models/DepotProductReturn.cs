using System;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductReturn
    {
        public int DepotProductReturnId { get; set; }
        public int DepotId { get; set; }
        public int ProductID { get; set; }
        public int DamageByRegistrationID { get; set; }
        public int Quantity { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Depot Depot { get; set; }
        public virtual Product Product { get; set; }
        public virtual Registration DamageByRegistration { get; set; }
    }
}