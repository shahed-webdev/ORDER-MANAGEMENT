using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class TargetVM
    {
        [Required(ErrorMessage = "Target Title is required!")]
        public string Target_Title { get; set; }

        [Required(ErrorMessage = "Start Date is required!")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required!")]
        [Display(Name = "End Date")]
        [DateGreaterThan("StartDate")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Target Amount is required!")]
        public double Total_TargetAmount { get; set; }
        public int CreatedByRegistrationID { get; set; }
    }

    public class TargetAssignVM
    {
        public TargetAssignVM()
        {
            this.Target_Ddl = new HashSet<Target_ddl>();
        }

        [Required(ErrorMessage = "Select Target !!")]
        public int TargetID { get; set; }

        [Required(ErrorMessage = "Target Amount Required !!")]
        public double? TargetAmount { get; set; }
        public UserVM user { get; set; }
        public UserTargetReport TargetReport { get; set; }

        public ICollection<Target_ddl> Target_Ddl { get; set; }
    }

    public class Target_ddl
    {
        public int TargetID { get; set; }
        public string Target_Title { get; set; }
    }

    public class GetTargetStatus
    {
        public double? Target { get; set; }
        public double? Achieved { get; set; }
        public double? Assinged { get; set; }
        public double? Assignable { get; set; }
        public bool Is_New_Target { get; set; }
        public double User_New_Target_Amount { get; set; }
        public double? User_Prev_Target_Amount { get; set; }
    }

    public class TargetAssignCreateVM
    {
        public bool Is_New_Target { get; set; }
        public int TargetID { get; set; }
        public int RegistrationID { get; set; }
        public float TargetAmount { get; set; }
    }


    public class TargetInfo
    {
        public double Target { get; set; }
        public double Achieved { get; set; }
        public double AchievedPercentage { get; set; }
    }
}