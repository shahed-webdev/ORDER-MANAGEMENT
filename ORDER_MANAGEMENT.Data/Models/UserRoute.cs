using System;

namespace ORDER_MANAGEMENT.Data
{
    public class UserRoute
    {
        public int UserRouteID { get; set; }
        public int RegistrationID { get; set; }
        public int? OutletID { get; set; }
        public int? DistributorID { get; set; }
        public string Type { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
        public virtual Outlet Outlet { get; set; }
        public virtual Distributor Distributor { get; set; }

    }
}
