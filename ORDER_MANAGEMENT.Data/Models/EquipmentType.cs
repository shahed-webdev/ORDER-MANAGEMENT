using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentType
    {
        public EquipmentType()
        {
            this.Equipments = new HashSet<Equipment>();
        }
        public int EquipmentTypeID { get; set; }

        [Required(ErrorMessage = "Type Name required !!")]
        public string EquipmentTypeName { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
    }

}
