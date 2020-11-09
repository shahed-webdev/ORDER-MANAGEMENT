using System;
using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    class UserTrackingByDistributorRepository : Repository<UserTrackingByDistributor>, IUserTrackingByDistributorRepository
    {
        public UserTrackingByDistributorRepository(DataContext context) : base(context)
        {

        }

        public void checkIn(UserTrackingByDistributor Tracking)
        {
            var t = Context.UserTrackingByDistributors.FirstOrDefault(u => u.User.RegistrationID == Tracking.RegistrationID && u.TrackingDate == DateTime.Today && u.DistributorID == Tracking.DistributorID);

            if (t == null)
            {
                Add(Tracking);
            }
        }

        public List<UserTrackingVM> TrackingUserWise(int RegID, DateTime Date)
        {
            var t = (from ut in Context.UserTrackingByDistributors
                     where ut.RegistrationID == RegID && ut.TrackingDate == Date.Date
                     orderby ut.TrackingDateTime
                     select new UserTrackingVM
                     {
                         Name = ut.Distributor.Name,
                         Address = ut.Distributor.Address,
                         Lat = ut.Distributor.Lat,
                         Lon = ut.Distributor.Lon,
                         Mobile = ut.Distributor.Mobile,
                         CheckInTime = ut.TrackingDateTime,
                         Photo = ut.Distributor.Photo
                     }).ToList();
            return t;
        }
    }
}
