using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public class Depot
    {
        public Depot()
        {
            DepotStocks = new HashSet<DepotStock>();
            DepotProductDamages = new HashSet<DepotProductDamage>();
            DepotProductReturns = new HashSet<DepotProductReturn>();
            DepotProductTransfers = new HashSet<DepotProductTransfer>();
            Distributors = new HashSet<Distributor>();
        }
        public int DepotId { get; set; }
        public string DepotName { get; set; }
        public int RegionID { get; set; }
        public int InchargeRegistrationId { get; set; }
        public virtual Region Region { get; set; }
        public virtual User User { get; set; }
        public DateTime? InsertDate { get; set; } = DateTime.Now;
        public virtual ICollection<DepotStock> DepotStocks { get; set; }
        public virtual ICollection<DepotProductTransfer> DepotProductTransfers { get; set; }
        public virtual ICollection<DepotProductReturn> DepotProductReturns { get; set; }
        public virtual ICollection<DepotProductDamage> DepotProductDamages { get; set; }
        public virtual ICollection<Distributor> Distributors { get; set; }
    }
}