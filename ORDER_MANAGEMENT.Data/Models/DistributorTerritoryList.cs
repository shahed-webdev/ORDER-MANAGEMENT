namespace ORDER_MANAGEMENT.Data
{
    public class DistributorTerritoryList
    {
        public int DistributorTerritoryListId { get; set; }
        public int DistributorID { get; set; }
        public int TerritoryID { get; set; }
        public virtual Territory Territory { get; set; }
        public virtual Distributor Distributor { get; set; }
    }
}