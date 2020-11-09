using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public partial class Area
    {
        public Area()
        {
            Territorys = new HashSet<Territory>();
        }
        public int AreaID { get; set; }

        [Required(ErrorMessage = "Area Name required !!")]
        public string AreaName { get; set; }

        [Required(ErrorMessage = "Region Name required !!")]
        public int RegionID { get; set; }
        public virtual Region Region { get; set; }

        public virtual ICollection<Territory> Territorys { get; set; }
    }
}
