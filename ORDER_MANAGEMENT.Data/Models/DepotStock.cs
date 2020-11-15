namespace ORDER_MANAGEMENT.Data
{
    public class DepotStock
    {
        public int DepotStockId { get; set; }
        public int DepotId { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int TotalTransfer { get; set; }
        public int TotalReturn { get; set; }
        public int TotalDamage { get; set; }
        public virtual Depot Depot { get; set; }
        public virtual Product Product { get; set; }
    }
}