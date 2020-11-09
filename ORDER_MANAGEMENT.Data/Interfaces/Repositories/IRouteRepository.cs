using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IRouteRepository : IRepository<Route>
    {
        void CreateRoute(RoutePlanMV model);

        IEnumerable<DDL> RouteDDL();
        IEnumerable<DDL> UnassignRouteUser();

        RoutePlanMV RouteDetails(int id);

    }
}
