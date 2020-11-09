using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IUserTrackingByOutletRepository : IRepository<UserTrackingByOutlet>
    {
        void checkIn(UserTrackingByOutlet userTracking);
        List<UserTrackingVM> TrackingUserWise(int RegID, DateTime Date);
    }
}
