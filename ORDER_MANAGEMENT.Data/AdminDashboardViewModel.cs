using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public class AdminDashboardViewModel
    {
        private readonly IUnitOfWork _db;
        public AdminDashboardViewModel(IUnitOfWork db)
        {
            _db = db;
            TotalDepot = _db.Depots.Count();
            TotalDistributor = _db.Distributors.Count();
            TotalOutlet = _db.Outlets.Count();
            RankWiseNumberOfUser = _db.Hierarchys.RankWiseNumberOfUser();
        }
        public int TotalDistributor { get; set; }
        public int TotalOutlet { get; set; }
        public int TotalDepot { get; set; }
        ICollection<RankWiseUser> RankWiseNumberOfUser { get; set; }
    }

    public class RankWiseUser
    {
        public int Rank { get; set; }
        public string HierarchyName { get; set; }
        public int UserCount { get; set; }

    }
}