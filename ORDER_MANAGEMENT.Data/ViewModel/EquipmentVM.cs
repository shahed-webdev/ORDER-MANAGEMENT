using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentVM
    {
        public int EquipmentID { get; set; }
        public int EquipmentTypeID { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentTypeName { get; set; }
        public string Code { get; set; }
        public string Size { get; set; }
    }

    public class EquipmentStatsVM : EquipmentVM
    {
        public bool IsDistributed { get; set; }
        public string OutletName { get; set; }
        public string ProprietorName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }


    public class EquipmentDetails : EquipmentVM
    {
        public EquipmentDistributionDetails CurrentOutlet { get; set; }
        public ICollection<EquipmentDistributionDetails> DistributionDetails { get; set; }

    }
    public class EquipmentDistributionDetails
    {
        public int EquipmentDistributionID { get; set; }
        public OutletSearch OutletInfo { get; set; }
        public string Location { get; set; }
        public DateTime InstalledDate { get; set; }
        public DateTime UninstalledDate { get; set; }
        public string TechnicianName { get; set; }
        public string InChargeName { get; set; }
        public double Value { get; set; }
        public string RentStatus { get; set; }
        public double RentPrice { get; set; }
        public string RentInterval { get; set; }
        public string AssignBy { get; set; }
    }
    public class EquipmentDistributionVM
    {
        public int EquipmentDistributionID { get; set; }

        [Required(ErrorMessage = "Equipment Type required !!")]
        public int EquipmentID { get; set; }

        [Required(ErrorMessage = "Outlet Type required !!")]
        public int OutletID { get; set; }
        [Required(ErrorMessage = "Assign By required !!")]
        public int AssignByRegistrationID { get; set; }
        public string Location { get; set; }

        [Required(ErrorMessage = "Installed Date required !!")]
        public DateTime? InstalledDate { get; set; }
        public string TechnicianName { get; set; }
        public string InChargeName { get; set; }



        [Required(ErrorMessage = "Equipment Value required !!")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Rent Status required !!")]
        public string RentStatus { get; set; }
        public double RentPrice { get; set; }
        public string RentInterval { get; set; }
    }
}
