using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORDER_MANAGEMENT.Data
{
    public class Outlet
    {
        public Outlet()
        {
            this.UserTrackingByOutlets = new HashSet<UserTrackingByOutlet>();
            this.Outlet_Images = new HashSet<Outlet_Images>();
            this.OutletOrders = new HashSet<OutletOrder>();
            this.OutletPaymentRecords = new HashSet<OutletPaymentRecord>();
            this.OutletProductDamages = new HashSet<OutletProductDamage>();
            this.OutletOrderChanges = new HashSet<OutletOrderChange>();
            this.UserRoutes = new HashSet<UserRoute>();
            this.EquipmentDistributions = new HashSet<EquipmentDistribution>();
        }
        public int OutletID { get; set; }
        public int TerritoryID { get; set; }
        public int DistributorID { get; set; }
        public string OutletName { get; set; }
        public string ProprietorName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int DueRangeLimit { get; set; } = 0;
        public bool IsApproved { get; set; } = false;
        public int? ApproveBy_RegistrationID { get; set; }
        public int CreateBy_RegistrationID { get; set; }
        public DateTime? ApproveDate { get; set; }
        public double Total_BuyingAmount { get; set; } = 0;
        public double Total_SellingAmount { get; set; } = 0;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Total_DueAmount { get; set; }
        public double Total_PaidAmount { get; set; } = 0;
        public double Total_ReturnAmount { get; set; } = 0;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public byte[] Logo { get; set; }
        public virtual Distributor Distributor { get; set; }
        public virtual Territory Territory { get; set; }
        public virtual ICollection<UserTrackingByOutlet> UserTrackingByOutlets { get; set; }
        public virtual ICollection<OutletOrder> OutletOrders { get; set; }
        public virtual ICollection<Outlet_Images> Outlet_Images { get; set; }
        public virtual ICollection<OutletPaymentRecord> OutletPaymentRecords { get; set; }
        public virtual ICollection<OutletProductDamage> OutletProductDamages { get; set; }
        public virtual ICollection<OutletOrderChange> OutletOrderChanges { get; set; }
        public virtual ICollection<UserRoute> UserRoutes { get; set; }
        public virtual ICollection<EquipmentDistribution> EquipmentDistributions { get; set; }

    }
}
