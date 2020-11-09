using System;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentDistribution
    {
        public int EquipmentDistributionID { get; set; }
        public int EquipmentID { get; set; }
        public int OutletID { get; set; }
        public string Location { get; set; }
        public DateTime InstalledDate { get; set; }
        public DateTime UninstalledDate { get; set; }
        public bool IsCurrent { get; set; } = true;
        public string TechnicianName { get; set; }
        public string InChargeName { get; set; }
        public int AssignByRegistrationID { get; set; }
        public double Value { get; set; }
        public string RentStatus { get; set; }
        public double RentPrice { get; set; }
        public string RentInterval { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Equipment Equipment { get; set; }
        public virtual Outlet Outlet { get; set; }
    }
}
