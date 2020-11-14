using System;

namespace ORDER_MANAGEMENT.Data
{
    public class Depot
    {
        public int DepotId { get; set; }
        public string DepotName { get; set; }
        public int RegionID { get; set; }
        public int InchargeRegistrationId { get; set; }
        public virtual Region Region { get; set; }
        public virtual User User { get; set; }
        public DateTime? InsertDate { get; set; } = DateTime.Now;
    }
}