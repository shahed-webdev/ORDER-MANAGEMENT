using System;

namespace ORDER_MANAGEMENT.Data
{
    public class ProductQuantityRecord
    {
        public int ProductQuantityRecordID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int InsertByRegistrationID { get; set; }
        public DateTime? InsertDate { get; set; } = DateTime.Now;
        public Product Product { get; set; }
    }
}
