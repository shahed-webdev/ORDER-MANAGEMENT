using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public partial class Region
    {
        public Region()
        {
            Areas = new HashSet<Area>();
            Depots = new HashSet<Depot>();
        }

        public int RegionID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Region Name required !!")]
        [Unique(ErrorMessage = "Region Name already exist !!", TargetModelType = typeof(Region), TargetPropertyName = "RegionName")]
        public string RegionName { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Depot> Depots { get; set; }
    }
}
