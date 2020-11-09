using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public partial class User
    {
        public User()
        {
            this.user_Territories = new HashSet<User_Territory>();
            this.TargetAssigns = new HashSet<TargetAssign>();
            this.TargetAssigns_To = new HashSet<TargetAssign>();
            this.Distributors = new HashSet<Distributor>();
            this.UserTrackingByDistributors = new HashSet<UserTrackingByDistributor>();
            this.UserTrackingByOutlets = new HashSet<UserTrackingByOutlet>();
            this.UserRoutes = new HashSet<UserRoute>();

        }
        public int RegistrationID { get; set; }
        public int HierarchyID { get; set; }
        public int? RouteID { get; set; }
        public int? DistributorID { get; set; }
        public int? Upper_RegistrationID { get; set; }
        public bool IsDefaultUser { get; set; } = false;
        public DateTime? InsertDate { get; set; } = DateTime.Now;
        public virtual Distributor Distributor { get; set; }
        public virtual Route Route { get; set; }
        public virtual Registration Registration { get; set; }
        public virtual Registration Upper_Registration { get; set; }
        public virtual ICollection<TargetAssign> TargetAssigns { get; set; }
        public virtual ICollection<TargetAssign> TargetAssigns_To { get; set; }
        public virtual ICollection<User_Territory> user_Territories { get; set; }
        public virtual ICollection<Distributor> Distributors { get; set; }
        public virtual ICollection<UserTrackingByDistributor> UserTrackingByDistributors { get; set; }
        public virtual ICollection<UserTrackingByOutlet> UserTrackingByOutlets { get; set; }
        public virtual ICollection<UserRoute> UserRoutes { get; set; }
        public virtual Organization_hierarchy Organization_hierarchy { get; set; }
    }
}
