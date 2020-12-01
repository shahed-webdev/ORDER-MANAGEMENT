using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public partial class Registration
    {
        public Registration()
        {
            this.Target_Creates = new HashSet<Target>();
            this.OrderBy_DistributorOrders = new HashSet<DistributorOrder>();
            this.ApproveBy_DistributorOrders = new HashSet<DistributorOrder>();
            this.DistributorOrderChanges = new HashSet<DistributorOrderChange>();
            this.ApproveBy_OutletOrders = new HashSet<OutletOrder>();
            this.OrderBy_OutletOrders = new HashSet<OutletOrder>();
            this.ApproveBy_DistributorProductReturns = new HashSet<DistributorProductReturn>();
            this.ReturnBy_DistributorProductReturns = new HashSet<DistributorProductReturn>();
            this.PageLinkAssigns = new HashSet<PageLinkAssign>();
            DepotProductDamages = new HashSet<DepotProductDamage>();
            DepotProductReturns = new HashSet<DepotProductReturn>();
            DepotProductTransfers = new HashSet<DepotProductTransfer>();
        }
        public int RegistrationID { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Designation { get; set; }
        public string Name { get; set; }
        public DateTime JoiningDate { get; set; } = DateTime.Now;
        public DateTime? DOB { get; set; }
        public string NID { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string OfficeContact { get; set; }
        public string PersonalContact { get; set; }
        public string HomeContact { get; set; }
        public string EmergencyContact { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string BloodGroup { get; set; }
        public DateTime? SignUpDate { get; set; } = DateTime.Now;
        public string PS { get; set; }
        public byte[] Photo { get; set; }
        public virtual User User { get; set; }

        public ICollection<Target> Target_Creates { get; set; }


        public ICollection<DistributorOrder> OrderBy_DistributorOrders { get; set; }
        public ICollection<DistributorOrder> ApproveBy_DistributorOrders { get; set; }
        public ICollection<DistributorOrderChange> DistributorOrderChanges { get; set; }
        public ICollection<DistributorProductReturn> ReturnBy_DistributorProductReturns { get; set; }
        public ICollection<DistributorProductReturn> ApproveBy_DistributorProductReturns { get; set; }
        public ICollection<PageLinkAssign> PageLinkAssigns { get; set; }
        public ICollection<OutletOrder> OrderBy_OutletOrders { get; set; }
        public ICollection<OutletOrder> ApproveBy_OutletOrders { get; set; }

        public virtual ICollection<DepotProductDamage> DepotProductDamages { get; set; }
        public virtual ICollection<DepotProductReturn> DepotProductReturns { get; set; }
        public virtual ICollection<DepotProductTransfer> DepotProductTransfers { get; set; }
    }
}
