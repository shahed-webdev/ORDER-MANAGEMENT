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

        public void Approved(int OutletID, int RegistrationID, int DueRange)
        {
            var Outlet = Find(OutletID);

            Outlet.ApproveBy_RegistrationID = RegistrationID;
            Outlet.ApproveDate = DateTime.Now;
            Outlet.IsApproved = true;
            Outlet.DueRangeLimit = DueRange;
            Update(Outlet);
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
