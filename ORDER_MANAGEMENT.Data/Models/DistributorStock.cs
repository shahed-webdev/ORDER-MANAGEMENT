namespace ORDER_MANAGEMENT.Data
{
    public class DistributorStock
    {
        public int DistributorStockID { get; set; }
        public int DistributorID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public virtual Distributor Distributor { get; set; }
        public virtual Product Product { get; set; }

    }
}
