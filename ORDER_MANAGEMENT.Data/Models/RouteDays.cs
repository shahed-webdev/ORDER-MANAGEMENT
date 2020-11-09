namespace ORDER_MANAGEMENT.Data
{
    public class RouteDays
    {
        public int RouteDayID { get; set; }
        public int RouteID { get; set; }
        public string Day { get; set; }
        public virtual Route Route { get; set; }
    }
}
