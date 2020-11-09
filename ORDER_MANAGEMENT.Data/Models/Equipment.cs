using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class Equipment
    {
        public Equipment()
        {
            this.EquipmentDistributions = new HashSet<EquipmentDistribution>();
        }
        public int EquipmentID { get; set; }
        [Required(ErrorMessage = "Equipment Type required !!")]
        public int EquipmentTypeID { get; set; }
        [Required(ErrorMessage = "Code required !!")]
        public string Code { get; set; }
        public string EquipmentName { get; set; }
        public string Size { get; set; }
        public virtual EquipmentType EquipmentType { get; set; }
        public virtual ICollection<EquipmentDistribution> EquipmentDistributions { get; set; }

    }
}
