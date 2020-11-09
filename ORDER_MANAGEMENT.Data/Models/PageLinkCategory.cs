using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkCategory
    {
        public PageLinkCategory()
        {
            this.PageLinks = new HashSet<PageLink>();
        }
        public int LinkCategoryID { get; set; }
        public string Category { get; set; }
        public string IconClass { get; set; }
        public int? SN { get; set; }
        public ICollection<PageLink> PageLinks { get; set; }
    }
}
