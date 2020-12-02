using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    class OutletRepository : Repository<Outlet>, IOutletRepository
    {
        public OutletRepository(DataContext context) : base(context)
        {

        }

        public void DueRangeChange(int OutletID, int DueRange)
        {
            var Outlet = Find(OutletID);
            Outlet.DueRangeLimit = DueRange;
            Update(Outlet);
        }

        public void Approved(int OutletID, int RegistrationID, int DueRange)
        {
            var Outlet = Find(OutletID);

            Outlet.ApproveBy_RegistrationID = RegistrationID;
            Outlet.ApproveDate = DateTime.Now;
            Outlet.IsApproved = true;
            Outlet.DueRangeLimit = DueRange;
            Update(Outlet);
        }

        public bool IsExist(string number)
        {
            return Context.Outlets.Any(o => o.Phone == number);
        }

        public void CreateOutlet(OutletCreateVM model)
        {
            var outlet = new Outlet();

            outlet.TerritoryID = model.TerritoryID;
            outlet.OutletName = model.OutletName;
            outlet.ProprietorName = model.ProprietorName;
            outlet.Phone = model.Phone;
            outlet.Address = model.Address;
            outlet.Email = model.Email;
            outlet.CreateBy_RegistrationID = model.CreateBy_RegistrationID;
            outlet.DistributorID = model.DistributorID;
            outlet.Lat = model.Lat;
            outlet.Lon = model.Lon;
            outlet.Logo = model.Logo;

            Add(outlet);
        }

        public DbResponse DeleteOutlet(int outletId)
        {
            try
            {
                var outlet = Context.Outlets.Find(outletId);
                if (outlet == null)
                    return new DbResponse(false, "Not Found");
                if (Context.OutletOrders.Any(o => o.OutletID == outletId))
                    return new DbResponse(false, "Order taken from this outlet");

                Context.Outlets.Remove(outlet);
                Context.SaveChanges();

                return new DbResponse(true, "Success");

            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }
        }

        public DbResponse UpdateDetails(OutletDetailsUpdateModel model)
        {
            try
            {
                var outlet = Context.Outlets.Find(model.OutletID);
                if (outlet == null)
                    return new DbResponse(false, "Not Found");

                if (Context.Outlets.Any(o => o.Phone == model.Phone && o.OutletID != model.OutletID))
                    return new DbResponse(false, $"{model.Phone} already exist");

                outlet.OutletName = model.OutletName;
                outlet.Phone = model.Phone;
                outlet.Email = model.Email;
                outlet.ProprietorName = model.ProprietorName;
                outlet.DueRangeLimit = model.DueRangeLimit;

                Context.Entry(outlet).State = EntityState.Modified;
                Context.SaveChanges();

                return new DbResponse(true, "Success");

            }
            catch (Exception e)
            {
                return new DbResponse(false, e.Message);
            }



        }

        public DbResponse<OutletDetailsUpdateModel> GetDetails(int outletId)
        {
            try
            {
                var outlet = Context.Outlets.Find(outletId);
                if (outlet == null)
                    return new DbResponse<OutletDetailsUpdateModel>(false, "Not Found");

                var data = new OutletDetailsUpdateModel
                {
                    OutletID = outlet.OutletID,
                    OutletName = outlet.OutletName,
                    ProprietorName = outlet.ProprietorName,
                    Phone = outlet.Phone,
                    Email = outlet.Email,
                    DueRangeLimit = outlet.DueRangeLimit
                };


                return new DbResponse<OutletDetailsUpdateModel>(true, "Success", data);

            }
            catch (Exception e)
            {
                return new DbResponse<OutletDetailsUpdateModel>(false, e.Message);
            }
        }

        public List<OutletListVM> OutletList()
        {
            var Outlets = from o in Context.Outlets
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
                          };
            return Outlets.ToList();
        }

        public List<OutletListVM> OutletListByUser(int registrationId)
        {
            var Outlets = from o in Context.Outlets
                          where o.CreateBy_RegistrationID == registrationId
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
                          };
            return Outlets.ToList();
        }

        public double GetDueRange(int OutletID)
        {

            var outlet = Find(OutletID);

            if (outlet == null) return 0;

            return outlet.DueRangeLimit;
        }

        public List<DDL> OutletByUserTerritory(int RegistrationID)
        {
            var Distributors = (from o in Context.Outlets
                                join u in Context.User_Territories
                                on o.TerritoryID equals u.TerritoryID
                                where u.RegistrationID == RegistrationID
                                select new DDL
                                {
                                    label = o.OutletName,
                                    value = o.OutletID
                                }).Distinct().ToList();

            return Distributors;
        }

        public List<DDL> OutletByTerritorys(List<int> TerritoryIDs)
        {
            var Distributors = (from o in Context.Outlets
                                where TerritoryIDs.Contains(o.TerritoryID)
                                select new DDL
                                {
                                    label = o.OutletName,
                                    value = o.OutletID
                                }).ToList();

            return Distributors;
        }

        public ICollection<OutletSearch> Search(string key)
        {
            return (from o in Context.Outlets
                    join t in Context.Territories on o.TerritoryID equals t.TerritoryID
                    join a in Context.Areas on t.AreaID equals a.AreaID
                    join r in Context.Regions on a.RegionID equals r.RegionID
                    where o.IsApproved && (o.OutletName.Contains(key) || a.AreaName.Contains(key) || t.TerritoryName.Contains(key))
                    select new OutletSearch
                    {
                        OutletID = o.OutletID,
                        OutletName = o.OutletName,
                        ProprietorName = o.ProprietorName,
                        Phone = o.Phone,
                        Address = o.Address,
                        RegionName = r.RegionName,
                        AreaName = a.AreaName,
                        TerritoryName = t.TerritoryName
                    }).Take(10).ToList();
        }

        public ICollection<OutletSearch> Search(string key, int currentOutletId)
        {
            return (from o in Context.Outlets
                    join t in Context.Territories on o.TerritoryID equals t.TerritoryID
                    join a in Context.Areas on t.AreaID equals a.AreaID
                    join r in Context.Regions on a.RegionID equals r.RegionID
                    where o.IsApproved && o.OutletID != currentOutletId && (o.OutletName.Contains(key) || a.AreaName.Contains(key) || t.TerritoryName.Contains(key))
                    select new OutletSearch
                    {
                        OutletID = o.OutletID,
                        OutletName = o.OutletName,
                        ProprietorName = o.ProprietorName,
                        Phone = o.Phone,
                        Address = o.Address,
                        RegionName = r.RegionName,
                        AreaName = a.AreaName,
                        TerritoryName = t.TerritoryName
                    }).Take(10).ToList();
        }
    }
}
