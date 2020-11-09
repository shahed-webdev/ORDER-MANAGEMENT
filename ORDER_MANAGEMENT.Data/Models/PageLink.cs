using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLink
    {
        public PageLink()
        {
            this.PageLinkAssigns = new HashSet<PageLinkAssign>();
        }
        public int LinkID { get; set; }
        public int LinkCategoryID { get; set; }
        public Guid? RoleId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public string IconClass { get; set; }
        public int? SN { get; set; }
        public virtual PageLinkCategory LinkCategory { get; set; }
        public ICollection<PageLinkAssign> PageLinkAssigns { get; set; }

    }
}
