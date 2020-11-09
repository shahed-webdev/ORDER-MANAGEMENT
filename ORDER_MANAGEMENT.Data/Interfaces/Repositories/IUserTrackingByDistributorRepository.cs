using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IUserTrackingByDistributorRepository : IRepository<UserTrackingByDistributor>
    {
        void checkIn(UserTrackingByDistributor userTracking);
        List<UserTrackingVM> TrackingUserWise(int RegID, DateTime Date);
    }
}
