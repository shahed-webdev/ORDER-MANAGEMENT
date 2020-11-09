using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public class Route
    {
        public Route()
        {
            this.Users = new HashSet<User>();
            this.RouteDays = new HashSet<RouteDays>();
        }
        public int RouteID { get; set; }
        public string RouteName { get; set; }
        public string PlanType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<RouteDays> RouteDays { get; set; }

    }
}
