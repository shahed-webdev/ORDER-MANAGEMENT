using System;
using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    class UserTrackingByOutletRepository : Repository<UserTrackingByOutlet>, IUserTrackingByOutletRepository
    {
        public UserTrackingByOutletRepository(DataContext context) : base(context)
        {

        }

        public void checkIn(UserTrackingByOutlet Tracking)
        {
            var t = Context.UserTrackingByOutlets.FirstOrDefault(u => u.User.RegistrationID == Tracking.RegistrationID && u.TrackingDate == DateTime.Today && u.OutletID == Tracking.OutletID);

            if (t == null)
            {
                Add(Tracking);
            }
        }

        public List<UserTrackingVM> TrackingUserWise(int RegID, DateTime Date)
        {
            var t = (from ut in Context.UserTrackingByOutlets
                     where ut.RegistrationID == RegID && ut.TrackingDate == Date.Date
                     orderby ut.TrackingDateTime
                     select new UserTrackingVM
                     {
                         Name = ut.Outlet.OutletName,
                         Address = ut.Outlet.Address,
                         Lat = ut.Outlet.Lat,
                         Lon = ut.Outlet.Lon,
                         Mobile = ut.Outlet.Phone,
                         CheckInTime = ut.TrackingDateTime,
                         Photo = ut.Outlet.Logo
                     }).ToList();
            return t;
        }
    }
}
