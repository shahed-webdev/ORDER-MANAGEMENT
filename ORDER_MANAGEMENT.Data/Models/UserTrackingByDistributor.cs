using System;

namespace ORDER_MANAGEMENT.Data
{
    public class UserTrackingByDistributor
    {
        public int TrackingByDistributorID { get; set; }
        public int RegistrationID { get; set; }
        public int DistributorID { get; set; }
        public string CheckIn_Lat { get; set; }
        public string CheckIn_Lon { get; set; }
        public DateTime TrackingDate { get; set; }
        public DateTime TrackingDateTime { get; set; }
        public virtual User User { get; set; }
        public virtual Distributor Distributor { get; set; }
    }
}
