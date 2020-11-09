using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkCategoryRepository : Repository<PageLinkCategory>, IPageLinkCategoryRepositoryRepository
    {
        public PageLinkCategoryRepository(DataContext context) : base(context)
        {

        }

        public ICollection<PageLinkCategory> GetCategoryWithLink()
        {
            return Context.PageLinkCategories.Include(p => p.PageLinks).OrderBy(p => p.SN).ToList();
        }

        public PageLink LinkRoleUpdate(int LinkID, Guid RoleId)
        {
            var pagelink = Context.PageLinks.Find(LinkID);
            pagelink.RoleId = RoleId;
            return pagelink;
        }

        public ICollection<DDL> ddl()
        {
            return Context.PageLinkCategories.Select(c => new DDL
            {
                value = c.LinkCategoryID,
                label = c.Category
            }).ToList();
        }
    }
}
