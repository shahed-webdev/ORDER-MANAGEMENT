using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class HierarchyVM
    {
        public int HierarchyID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Hierarchy Name required !!")]
        [Unique(ErrorMessage = "Hierarchy Name already exist !!", TargetModelType = typeof(Organization_hierarchy), TargetPropertyName = "HierarchyName")]
        public string HierarchyName { get; set; }

        [Unique(ErrorMessage = "Rank already exist !!", TargetModelType = typeof(Organization_hierarchy), TargetPropertyName = "Rank")]
        [Required(ErrorMessage = "Rank required !!")]
        public int Rank { get; set; }
        public int? CreateRegistrationID { get; set; }
        public List<Organization_hierarchy> Hierarachys { get; set; }
    }


    public class HierarchyDll_VM
    {
        public int Rank { get; set; }
        public string HierarchyName { get; set; }
    }
}