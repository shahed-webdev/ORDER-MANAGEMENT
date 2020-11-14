using System;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductTransfer
    {
        public int DepotProductTransferId { get; set; }
        public int DepotId { get; set; }
        public int ProductID { get; set; }
        public int TransferByRegistrationID { get; set; }
        public int Quantity { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Depot Depot { get; set; }
        public virtual Product Product { get; set; }
        public virtual Registration TransferByRegistration { get; set; }
    }
}