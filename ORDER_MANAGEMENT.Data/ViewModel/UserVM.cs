using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Data

{
    public class CreateUserVM
    {
        public CreateUserVM()
        {
            using (var db = new DataContext())
            {
                Hierarchies = db.Organization_hierarchy.Where(h => h.Rank > 1).Select(n => new SelectListItem { Value = n.Rank.ToString(), Text = n.HierarchyName }).ToList();
            }
        }

        [Required(ErrorMessage = "User is required!")]
        public int UpperRegistrationID { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string UserName { get; set; }

        public string Designation { get; set; }

        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime JoiningDate { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime DOB { get; set; }
        public string NID { get; set; }
        public string FatherName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string OfficeContact { get; set; }

        [Required(ErrorMessage = "Personal Contact is required!")]
        public string PersonalContact { get; set; }

        public string OfficeEmail { get; set; }

        [Required(ErrorMessage = "Personal Email is required!")]
        public string PersonalEmail { get; set; }

        [Required(ErrorMessage = "Hierarchy is required!")]
        public int Rank { get; set; }

        public IEnumerable<SelectListItem> Hierarchies { get; set; }
        public byte[] Photo { get; set; }
        public IEnumerable<SelectListItem> Territorys { get; set; }

        [Required(ErrorMessage = "Territory is required!")]
        public int[] TerritoryIDs { get; set; }
        public string PS { get; set; }
    }

    public class UpdateUserVM
    {
        public UpdateUserVM()
        {
            this.user = new User();
        }
        public UpdateUserVM(User user)
        {
            this.user = user;
            this.Rank = user.Organization_hierarchy.Rank;
            this.UpperRegistrationID = user.Upper_RegistrationID.GetValueOrDefault();

        }

        public User user { get; set; }
        public IEnumerable<SelectListItem> Hierarchies { get; set; }
        public int Rank { get; set; }
        public IEnumerable<SelectListItem> UpperUsers { get; set; }
        public int UpperRegistrationID { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; }
        public int[] RegionIDs { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }
        public int[] AreaIDs { get; set; }
        public IEnumerable<SelectListItem> Territorys { get; set; }
        public int[] TerritoryIDs { get; set; }
    }


    public class UserSR
    {
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public string NID { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalContact { get; set; }
        public string ReportingTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime JoiningDate { get; set; }

        public string AssingedToDistributor { get; set; }
    }

    public class UserVM
    {
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string HierarchyName { get; set; }
        public int Rank { get; set; }
        public string Designation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime? DOB { get; set; }
        public string NID { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalContact { get; set; }
        public string ReportingTo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime JoiningDate { get; set; }

    }

    public class UserddlVM
    {
        public int RegistrationID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }

    public class UserDashboardVM
    {
        public UserDashboardVM()
        {
            this.ReportFrom = new HashSet<UserReportFrom>();
        }
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string HierarchyName { get; set; }
        public int Rank { get; set; }
        public string Designation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime? DOB { get; set; }
        public string NID { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalContact { get; set; }
        public byte[] Photo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        public DateTime JoiningDate { get; set; }
        public bool Is_default_User { get; set; }
        public UserReportTo ReportTo { get; set; }
        public UserTargetReport TargetReport { get; set; }
        public ICollection<UserReportFrom> ReportFrom { get; set; }

    }

    public class UserReportTo
    {
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public string HierarchyName { get; set; }
        public int Rank { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalContact { get; set; }
    }

    public class UserReportFrom
    {
        private int _equipment;
        public int RegistrationID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string HierarchyName { get; set; }
        public int Rank { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalContact { get; set; }
        public double? TargetAmount { get; set; }
        public double? AchievedAmount { get; set; }
        public double PerEquipmentAmount { get; private set; }

        public int Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;

                PerEquipmentAmount = AchievedAmount ?? 0 / (_equipment == 0 ? 1 : _equipment);
            }
        }
    }

    public class UserTargetReport
    {
        public double? Target { get; set; }
        public double? Achieved { get; set; }
        public double? Achieved_Percentage { get; set; }
        public double? Assinged { get; set; }
        public double? Assignable { get; set; }
        public double? Assinged_Percentage { get; set; }
    }

    public class UserNameRank
    {
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string HierarchyName { get; set; }
        public string PersonalEmail { get; set; }
        public string PresentAddress { get; set; }
    }

    public class UserInfoUpdate
    {
        public byte[] Photo { get; set; }
        public string PersonalEmail { get; set; }
        public string PresentAddress { get; set; }
    }

    public class AdminBasic
    {
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public byte[] Photo { get; set; }
    }
    public class AdminInfo : AdminBasic
    {
        public DateTime JoiningDate { get; set; } = DateTime.Now;
        public DateTime? DOB { get; set; }
        public string NID { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string OfficeContact { get; set; }
        public string PersonalContact { get; set; }
        public string HomeContact { get; set; }
        public string EmergencyContact { get; set; }
        public string OfficeEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string BloodGroup { get; set; }
    }
    public class UserDetails : AdminInfo
    {
        public string HierarchyName { get; set; }
        public int Rank { get; set; }
        public string Designation { get; set; }
    }
    public class DistributorDetails : AdminInfo
    {
        public int DueRangeLimit { get; set; }

        public int DistributorID { get; set; }
    }
}