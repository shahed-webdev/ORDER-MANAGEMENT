using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorRepository : Repository<Distributor>, IDistributorRepository
    {
        public DistributorRepository(DataContext context) : base(context)
        {

        }
        public void CreateDistributor(DistributorCreateVM model)
        {
            var dis = new Distributor();

            dis.TerritoryID = model.TerritoryID;
            dis.ReportTo_RegistrationID = model.ReportTo_RegistrationID;
            dis.Name = model.Name;
            dis.Mobile = model.Mobile;
            dis.Address = model.Address;
            dis.Lat = model.Lat;
            dis.Lon = model.Lon;
            dis.Photo = model.Photo;

            Add(dis);
        }
        public List<DistributorListWithUserVM> DistributorListWithUser()
        {
            var Distributors = (from d in Context.Distributors
                                orderby d.DistributorID descending
                                select new DistributorListWithUserVM
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
                                    IsApproved = d.IsApproved,
                                    ReportTo_User = d.ReportTo_User.Registration,
                                    TerritoryName = d.Territory.TerritoryName
                                }).ToList();
            return Distributors;
        }
        public List<DistributorListVM> DistributorListByUser(int id)
        {
            var Distributors = (from d in Context.Distributors
                                where d.ReportTo_RegistrationID == id
                                orderby d.DistributorID descending
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
        public List<DistributorListVM> DistributorList()
        {
            var Distributors = (from d in Context.Distributors
                                orderby d.DistributorID descending
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
        public void Approved(int ApproveBy_RegistrationID, DistirubtorRegisterVM Reg, Registration r)
        {
            var distributor = Find(Reg.DistributorID);

            distributor.ApproveBy_RegistrationID = ApproveBy_RegistrationID;
            distributor.ApproveDate = DateTime.Now;
            distributor.IsApproved = true;
            distributor.DueRangeLimit = Reg.DueRange;
            distributor.Registration = r;
            Update(distributor);
        }
        public int DistributerCount(DistributorTypes Types)
        {
            int conut = 0;
            if (Types == DistributorTypes.All)
                conut = Context.Distributors.Count();
            else if (Types == DistributorTypes.Aapproved)
                conut = Context.Distributors.Count(d => d.IsApproved);
            else if (Types == DistributorTypes.unapproved)
                conut = Context.Distributors.Count(d => !d.IsApproved);
            return conut;
        }
        public double GetDueRange(int DistributorID)
        {

            var dis = Find(DistributorID);

            if (dis == null) return 0;

            return dis.DueRangeLimit;
        }
        public List<DDL> DistributorByUserTerritory(int RegistrationID)
        {
            var Distributors = (from d in Context.Distributors
                                join u in Context.User_Territories
                                on d.TerritoryID equals u.TerritoryID
                                where u.RegistrationID == RegistrationID
                                select new DDL
                                {
                                    label = d.Name,
                                    value = d.DistributorID
                                }).Distinct().ToList();

            return Distributors;
        }

        public DistributorDetails GetDetails(int? DistributorID)
        {
            if (DistributorID == null) return null;

            return Context.Distributors.Include(d => d.Registration).Where(d => d.DistributorID == DistributorID).Select(d => new DistributorDetails
            {
                DistributorID = d.DistributorID,
                Name = d.Registration.Name,
                Photo = d.Registration.Photo,
                RegistrationID = d.Registration.RegistrationID,
                Type = d.Registration.Type,
                BloodGroup = d.Registration.BloodGroup,
                DOB = d.Registration.DOB,
                EmergencyContact = d.Registration.EmergencyContact,
                FatherName = d.Registration.FatherName,
                HomeContact = d.Registration.HomeContact,
                JoiningDate = d.Registration.JoiningDate,
                MotherName = d.Registration.MotherName,
                NID = d.Registration.NID,
                OfficeContact = d.Registration.OfficeContact,
                OfficeEmail = d.Registration.OfficeEmail,
                PermanentAddress = d.Registration.PermanentAddress,
                PersonalContact = d.Registration.PersonalContact,
                PersonalEmail = d.Registration.PersonalEmail,
                PresentAddress = d.Registration.PresentAddress,
                DueRangeLimit = d.DueRangeLimit

            }).FirstOrDefault();
        }

        public void UpdateDetails(DistributorDetails dis)
        {
            var d = Context.Distributors.Include(ds => ds.Registration).Where(ds => ds.DistributorID == dis.DistributorID).FirstOrDefault();
            d.Registration.Name = dis.Name;
            d.Name = dis.Name;
            if (dis.Photo != null)
                d.Registration.Photo = dis.Photo;
            d.Registration.BloodGroup = dis.BloodGroup;
            d.Registration.EmergencyContact = dis.EmergencyContact;
            d.Registration.FatherName = dis.FatherName;
            d.Registration.HomeContact = dis.HomeContact;
            d.Registration.MotherName = dis.MotherName;
            d.Registration.NID = dis.NID;
            d.Registration.OfficeContact = dis.OfficeContact;
            d.Registration.OfficeEmail = dis.OfficeEmail;
            d.Registration.PermanentAddress = dis.PermanentAddress;
            d.Registration.PresentAddress = dis.PresentAddress;
            d.Address = dis.PresentAddress;
            d.Registration.PersonalContact = dis.PersonalContact;
            d.Mobile = dis.PersonalContact;
            d.Registration.PersonalEmail = dis.PersonalEmail;
            d.DueRangeLimit = dis.DueRangeLimit;

            Context.Entry(d).State = EntityState.Modified;
        }

        public List<DDL> DistributorByTerritorys(ICollection<int> TerritoryIDs)
        {
            var Distributors = (from d in Context.Distributors
                                where TerritoryIDs.Any(id => id.Equals(d.TerritoryID))
                                select new DDL
                                {
                                    label = d.Name,
                                    value = d.DistributorID
                                }).Distinct().ToList();


            return Distributors;
        }

        public void AssignSR(int DistributorID, ICollection<int> SR_IDs)
        {

            var users = Context.Users.Where(u => SR_IDs.Any(id => id.Equals(u.RegistrationID))).ToList();
            foreach (var user in users)
            {
                user.DistributorID = DistributorID;
                Context.Entry(user).State = EntityState.Modified;
            }


        }

        public void AssignDepot(int distributorId, int depotId)
        {
            var distributor = Context.Distributors.Find(distributorId);

            if (distributor == null) return;

            distributor.DepotId = depotId;
            Context.Entry(distributor).State = EntityState.Modified;

        }
    }
}
