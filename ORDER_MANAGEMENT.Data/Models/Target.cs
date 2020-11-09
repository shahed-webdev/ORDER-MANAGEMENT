using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class Target
    {
        public Target()
        {
            this.TargetAssigns = new HashSet<TargetAssign>();
        }
        public int TargetID { get; set; }
        public string Target_Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public double Total_TargetAmount { get; set; }
        public double Total_AchievedAmount { get; set; } = 0;
        public int CreatedByRegistrationID { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Registration Registration { get; set; }
        public virtual ICollection<TargetAssign> TargetAssigns { get; set; }
    }
}