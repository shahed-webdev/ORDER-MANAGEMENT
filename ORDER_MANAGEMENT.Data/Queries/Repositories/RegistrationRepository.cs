using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(DataContext context) : base(context)
        {

        }

        public AdminBasic GetAdminBasic(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return null;

            return Context.Registrations.Where(r => r.UserName == UserName).Select(r => new AdminBasic
            {
                Name = r.Name,
                Photo = r.Photo,
                RegistrationID = r.RegistrationID,
                Type = r.Type
            }).FirstOrDefault();
        }

        public AdminInfo GetAdminInfo(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return null;

            return Context.Registrations.Where(r => r.UserName == UserName).Select(r => new AdminInfo
            {
                Name = r.Name,
                Photo = r.Photo,
                RegistrationID = r.RegistrationID,
                Type = r.Type,
                BloodGroup = r.BloodGroup,
                DOB = r.DOB,
                EmergencyContact = r.EmergencyContact,
                FatherName = r.FatherName,
                HomeContact = r.HomeContact,
                JoiningDate = r.JoiningDate,
                MotherName = r.MotherName,
                NID = r.NID,
                OfficeContact = r.OfficeContact,
                OfficeEmail = r.OfficeEmail,
                PermanentAddress = r.PermanentAddress,
                PersonalContact = r.PersonalContact,
                PersonalEmail = r.PersonalEmail,
                PresentAddress = r.PresentAddress
            }).FirstOrDefault();
        }

        public string GetRankName_ByUserName(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return "";
            return Context.Registrations.Include(r => r.User.Organization_hierarchy).FirstOrDefault(r => r.UserName == UserName).User.Organization_hierarchy.HierarchyName;
        }

        public int GetRegID_ByUserName(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return -1;

            return Context.Registrations.FirstOrDefault(r => r.UserName == UserName).RegistrationID;
        }

        public ICollection<AdminInfo> GetSubAdminList()
        {
            return Context.Registrations.Where(r => r.Type == "Sub-Admin").Select(r => new AdminInfo
            {
                UserName = r.UserName,
                Name = r.Name,
                Photo = r.Photo,
                RegistrationID = r.RegistrationID,
                Type = r.Type,
                BloodGroup = r.BloodGroup,
                DOB = r.DOB,
                EmergencyContact = r.EmergencyContact,
                FatherName = r.FatherName,
                HomeContact = r.HomeContact,
                JoiningDate = r.JoiningDate,
                MotherName = r.MotherName,
                NID = r.NID,
                OfficeContact = r.OfficeContact,
                OfficeEmail = r.OfficeEmail,
                PermanentAddress = r.PermanentAddress,
                PersonalContact = r.PersonalContact,
                PersonalEmail = r.PersonalEmail,
                PresentAddress = r.PresentAddress
            }).ToList();
        }

        public Registration Reg_Update(string userName, UserDetails reg)
        {
            var r = Context.Registrations.FirstOrDefault(u => u.UserName == userName);
            r.BloodGroup = reg.BloodGroup;
            r.EmergencyContact = reg.EmergencyContact;
            r.FatherName = reg.FatherName;
            r.HomeContact = reg.HomeContact;
            r.MotherName = reg.MotherName;
            r.Name = reg.Name;
            r.OfficeContact = reg.OfficeContact;
            r.OfficeEmail = reg.OfficeEmail;
            r.PermanentAddress = reg.PermanentAddress;
            r.PersonalContact = reg.PersonalContact;
            r.PersonalEmail = reg.PersonalEmail;
            if (reg.Photo != null)
                r.Photo = reg.Photo;
            r.PresentAddress = reg.PresentAddress;

            return r;
        }

        public ICollection<DDL> SubAdmins()
        {
            return Context.Registrations.Where(r => r.Type == "Sub-Admin").Select(r => new DDL { value = r.RegistrationID, label = r.Name + " (" + r.UserName + ")" }).ToList();
        }
    }
}