using System;

namespace ORDER_MANAGEMENT.Data
{
    public class UserTrackingVM
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public DateTime CheckInTime { get; set; }
        public byte[] Photo { get; set; }
    }
}
