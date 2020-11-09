using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IUserRouteRepository : IRepository<UserRoute>
    {
        void AddUserInRoute(UserRoutePoint points);
        List<UserRouteVM> RouteUserList();
        List<DistributorListVM> getDistributorByUserRoute(int RegID);
        List<OutletListVM> getOutletByUserRoute(int RegID);
    }
}
