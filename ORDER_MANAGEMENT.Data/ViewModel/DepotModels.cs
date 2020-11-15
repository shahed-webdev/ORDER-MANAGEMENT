namespace ORDER_MANAGEMENT.Data
{
    public class DepotViewModel
    {
        public int DepotId { get; set; }
        public string DepotName { get; set; }
        public string RegionName { get; set; }
        public string Incharge { get; set; }
    }

    public class DepotStockAdd
    {
        public int DepotId { get; set; }
        public int ProductID { get; set; }
        public int RegistrationID { get; set; }
        public int Quantity { get; set; }
    }
}