using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public partial class Organization_hierarchy
    {
        public Organization_hierarchy()
        {
            this.Users = new HashSet<User>();
        }
        public int HierarchyID { get; set; }

        [Required(ErrorMessage = "Hierarchy Name required !!")]
        [Unique(ErrorMessage = "Hierarchy Name already exist !!", TargetModelType = typeof(Organization_hierarchy), TargetPropertyName = "HierarchyName")]
        public string HierarchyName { get; set; }

        [Required(ErrorMessage = "Rank required !!")]
        [Unique(ErrorMessage = "Rank already exist !!", TargetModelType = typeof(Organization_hierarchy), TargetPropertyName = "Rank")]
        public int Rank { get; set; }

        public ICollection<User> Users { get; set; }
        public int? CreateRegistrationID { get; set; }
        public DateTime? InsertDate { get; set; } = DateTime.Now;
    }
}
