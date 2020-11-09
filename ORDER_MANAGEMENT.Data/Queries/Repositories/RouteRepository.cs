using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class RouteRepository : Repository<Route>, IRouteRepository
    {
        public RouteRepository(DataContext context) : base(context)
        {

        }

        public void CreateRoute(RoutePlanMV model)
        {
            var route = new Route
            {
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                IsActive = model.IsActive,
                PlanType = model.PlanType,
                RouteName = model.RouteName,
                RouteDays = model.SelectedDays.Select(d => new RouteDays { Day = d }).ToList()
            };
            Add(route);

        }

        public IEnumerable<DDL> RouteDDL()
        {
            return GetAll().Select(r => new DDL { label = r.RouteName, value = r.RouteID });
        }

        public IEnumerable<DDL> UnassignRouteUser()
        {
            return Context.Users.Where(u => u.RouteID == null).Select(u => new DDL { label = u.Registration.Name + " (" + u.Organization_hierarchy.HierarchyName + ")", value = u.RegistrationID });
        }

        public void SelectedRouteUser()
        {

        }

        public RoutePlanMV RouteDetails(int id)
        {

            var route = Context.Routes.Include(r => r.RouteDays).SingleOrDefault(r => r.RouteID == id);
            RoutePlanMV rp = new RoutePlanMV
            {
                RouteName = route.RouteName,
                StartDate = route.StartDate,
                EndDate = route.EndDate,
                IsActive = route.IsActive,
                PlanType = route.PlanType,
                SelectedDays = route.RouteDays.Select(r => r.Day).ToArray()
            };
            return rp;
        }
    }
}
