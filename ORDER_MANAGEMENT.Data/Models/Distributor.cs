using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORDER_MANAGEMENT.Data
{
    public class Distributor
    {
        public Distributor()
        {
            this.DistributorStocks = new HashSet<DistributorStock>();
            this.DistributorOrders = new HashSet<DistributorOrder>();
            this.DistributorPaymentRecords = new HashSet<DistributorPaymentRecord>();
            this.DistributorProductDamages = new HashSet<DistributorProductDamage>();
            this.DistributorOrderChanges = new HashSet<DistributorOrderChange>();
            this.UserTrackingByDistributors = new HashSet<UserTrackingByDistributor>();
            this.OutletOrders = new HashSet<OutletOrder>();
            this.UserRoutes = new HashSet<UserRoute>();
            this.Users = new HashSet<User>();
            this.Outlets = new HashSet<Outlet>();
        }
        public int DistributorID { get; set; }
        public int TerritoryID { get; set; }
        public int ReportTo_RegistrationID { get; set; }
        public int? RegistrationID { get; set; }
        public int? DepotId { get; set; }
        public double DiscountPercentage { get; set; } = 0;
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int DueRangeLimit { get; set; } = 0;
        public bool IsApproved { get; set; } = false;
        public int? ApproveBy_RegistrationID { get; set; }
        public DateTime? ApproveDate { get; set; }
        public double Total_BuyingAmount { get; set; } = 0;
        public double Total_SellingAmount { get; set; } = 0;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double Total_DueAmount { get; set; }
        public double Total_PaidAmount { get; set; } = 0;
        public double Total_ReturnAmount { get; set; } = 0;
        public byte[] Photo { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Territory Territory { get; set; }
        public virtual User ReportTo_User { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual Depot Depot { get; set; }
        public virtual ICollection<DistributorStock> DistributorStocks { get; set; }
        public virtual ICollection<DistributorOrder> DistributorOrders { get; set; }
        public virtual ICollection<DistributorPaymentRecord> DistributorPaymentRecords { get; set; }
        public virtual ICollection<DistributorProductDamage> DistributorProductDamages { get; set; }
        public virtual ICollection<DistributorOrderChange> DistributorOrderChanges { get; set; }
        public virtual ICollection<UserTrackingByDistributor> UserTrackingByDistributors { get; set; }
        public virtual ICollection<OutletOrder> OutletOrders { get; set; }

        public virtual ICollection<UserRoute> UserRoutes { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Outlet> Outlets { get; set; }

    }
}
