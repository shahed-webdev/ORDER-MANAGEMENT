using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class Territory
    {
        public Territory()
        {
            this.user_Territories = new HashSet<User_Territory>();
            this.Distributors = new HashSet<Distributor>();
            this.Outlets = new HashSet<Outlet>();
        }
        public int TerritoryID { get; set; }

        [Required(ErrorMessage = "Area Name required !!")]
        public int AreaID { get; set; }

        [Required(ErrorMessage = "Territory Name required !!")]
        public string TerritoryName { get; set; }
        public double DistributorDiscountPercentage { get; set; } = 0;
        public virtual Area Area { get; set; }
        public virtual ICollection<User_Territory> user_Territories { get; set; }
        public virtual ICollection<Distributor> Distributors { get; set; }
        public virtual ICollection<Outlet> Outlets { get; set; }
        public virtual ICollection<DistributorTerritoryList> DistributorTerritoryLists { get; set; }
    }
}