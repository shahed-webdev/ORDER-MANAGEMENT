using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkRepository : Repository<PageLink>, IPageLinkRepository
    {
        public PageLinkRepository(DataContext context) : base(context)
        {

        }

        public List<SideMenuCategory> GetSideMenuByUser(string UserName)
        {

            if (string.IsNullOrEmpty(UserName)) return null;

            var Reg = Context.Registrations.FirstOrDefault(r => r.UserName == UserName);
            if (Reg.Type == "Admin")
            {
                var Meun = (from p in Context.PageLinkCategories
                                //join u in Context.Users
                                //on r.RegistrationID equals u.RegistrationID
                                //join h in Context.Organization_hierarchy
                                //on u.HierarchyID equals h.HierarchyID
                                //where h.Rank < Rank && u.RegistrationID != RegistrationID
                            orderby p.SN
                            select new SideMenuCategory
                            {
                                LinkCategoryID = p.LinkCategoryID,
                                Category = p.Category,
                                IconClass = p.IconClass,
                                SN = p.SN,
                                links = p.PageLinks.Select(l => new SideMenuLink
                                {
                                    LinkID = l.LinkID,
                                    SN = l.SN,
                                    Action = l.Action,
                                    Controller = l.Controller,
                                    IconClass = l.IconClass,
                                    Title = l.Title
                                }).OrderBy(l => l.SN).ToList()
                            }).ToList();
                return Meun;
            }
            else
            {
                var Meun = (from p in Context.PageLinkAssigns
                            join c in Context.PageLinkCategories
                            on p.PageLink.LinkCategory.LinkCategoryID equals c.LinkCategoryID
                            where p.RegistrationID == Reg.RegistrationID
                            orderby p.PageLink.LinkCategory.SN
                            select new SideMenuCategory
                            {
                                LinkCategoryID = c.LinkCategoryID,
                                Category = c.Category,
                                IconClass = c.IconClass,
                                SN = c.SN
                            }).Distinct().ToList();

                foreach (var item in Meun)
                {
                    item.links = Context.PageLinkAssigns.Where(l => l.PageLink.LinkCategoryID == item.LinkCategoryID && l.RegistrationID == Reg.RegistrationID).Select(l => new SideMenuLink
                    {
                        LinkID = l.PageLink.LinkID,
                        SN = l.PageLink.SN,
                        Action = l.PageLink.Action,
                        Controller = l.PageLink.Controller,
                        IconClass = l.PageLink.IconClass,
                        Title = l.PageLink.Title
                    }).ToList();
                }

                return Meun;
            }

        }
    }
}
