using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class UserRouteRepository : Repository<UserRoute>, IUserRouteRepository
    {
        public UserRouteRepository(DataContext context) : base(context)
        {

        }

        public void AddUserInRoute(UserRoutePoint data)
        {
            var userPoints = data.points.Select(p => new UserRoute
            {
                RegistrationID = data.RegistrationID,
                Type = p.Type,
                DistributorID = p.Type == "Distributor" ? p?.ID : null,
                OutletID = p.Type == "Outlet" ? p?.ID : null
            }).ToList();

            AddRange(userPoints);
        }

        public List<UserRouteVM> RouteUserList()
        {
            var routes = (from u in Context.Users
                          where u.RouteID != null
                          select new UserRouteVM
                          {
                              RegistrationID = u.RegistrationID,
                              UserName = u.Registration.Name,
                              RouteID = u.RouteID,
                              RouteDetails = new RoutePlanMV
                              {
                                  EndDate = u.Route.EndDate,
                                  IsActive = u.Route.IsActive,
                                  PlanType = u.Route.PlanType,
                                  RouteName = u.Route.RouteName,
                                  StartDate = u.Route.StartDate,
                                  SelectedDays = u.Route.RouteDays.Select(rd => rd.Day)



                              },
                              points = u.UserRoutes.Select(
                                  ur => new Point
                                  {
                                      ID = ur.Type == "Distributor" ? ur.DistributorID : ur.OutletID,
                                      Name = ur.Type == "Distributor" ? ur.Distributor.Name : ur.Outlet.OutletName,
                                      Type = ur.Type
                                  }).ToList()
                          }).ToList();

            return routes;
        }

        public List<DistributorListVM> getDistributorByUserRoute(int RegID)
        {
            var route = Context.Users.Include(u => u.Route).FirstOrDefault(u => u.RegistrationID == RegID).Route;

            if (route == null) return null;

            var todayDate = DateTime.Today;
            var weekday = DateTime.Now.ToString("ddd");
            bool IsValid = false;

            if (route.PlanType == "Weekly")
            {
                IsValid = Context.RouteDays.Any(rd => rd.RouteID == route.RouteID && rd.Day == weekday);
            }
            else
            {
                IsValid = route.StartDate <= todayDate && todayDate <= route.EndDate;
            }
            if (!IsValid) return null;

            var Distributors = (from u in Context.UserRoutes
                                join d in Context.Distributors
                                on u.DistributorID equals d.DistributorID
                                orderby d.DistributorID descending
                                where u.RegistrationID == RegID
                                select new DistributorListVM
                                {
                                    Address = d.Address,
                                    DueRangeLimit = d.DueRangeLimit,
                                    InsertDate = d.InsertDate,
                                    Lat = d.Lat,
                                    Lon = d.Lon,
                                    Mobile = d.Mobile,
                                    Name = d.Name,
                                    TotalDue = d.Total_DueAmount,
                                    Photo = d.Photo,
                                    DistributorID = d.DistributorID,
                                    IsApproved = d.IsApproved
                                }).ToList();
            return Distributors;
        }

        public List<OutletListVM> getOutletByUserRoute(int RegID)
        {
            var route = Context.Users.Include(u => u.Route).FirstOrDefault(u => u.RegistrationID == RegID).Route;
            if (route == null) return null;
            var todayDate = DateTime.Today;
            var weekday = DateTime.Now.ToString("ddd");
            bool IsValid = false;

            if (route.PlanType == "Weekly")
            {
                IsValid = Context.RouteDays.Any(rd => rd.RouteID == route.RouteID && rd.Day == weekday);
            }
            else
            {
                IsValid = route.StartDate <= todayDate && todayDate <= route.EndDate;
            }
            if (!IsValid) return null;

            var Outlets = (from u in Context.UserRoutes
                           join o in Context.Outlets
                           on u.OutletID equals o.OutletID
                           orderby o.OutletID descending
                           where u.RegistrationID == RegID
                           select new OutletListVM()
                           {
                               Address = o.Address,
                               ProprietorName = o.ProprietorName,
                               DueRangeLimit = o.DueRangeLimit,
                               InsertDate = o.InsertDate,
                               Lat = o.Lat,
                               Lon = o.Lon,
                               Phone = o.Phone,
                               OutletName = o.OutletName,
                               TotalDue = o.Total_DueAmount,
                               Logo = o.Logo,
                               OutletID = o.OutletID,
                               IsApproved = o.IsApproved
                           }).ToList();
            return Outlets;
        }
    }
}
