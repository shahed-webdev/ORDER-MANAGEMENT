using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class PageLinkAssignRepository : Repository<PageLinkAssign>, IPageLinkAssignRepository
    {
        public PageLinkAssignRepository(DataContext context) : base(context)
        {

        }

        public string AssignLink(int RegID, ICollection<PageAssignVM> Links)
        {
            var p_assigns = Links.Select(l => new PageLinkAssign
            {
                LinkID = l.LinkID,
                RegistrationID = RegID
            }).ToList();
            var p_delete = Context.PageLinkAssigns.Where(p => p.RegistrationID == RegID).ToList();
            Context.PageLinkAssigns.RemoveRange(p_delete);
            Context.PageLinkAssigns.AddRange(p_assigns);
            return Context.Registrations.Find(RegID).UserName;
        }

        public ICollection<PageCategoryVM> SubAdminLinks(int RegID)
        {

            var user_dll = (from c in Context.PageLinkCategories
                            select new PageCategoryVM
                            {
                                Category = c.Category,
                                Links = (from l in Context.PageLinks
                                         join r in Context.AspNetRoles
                                         on l.RoleId equals r.Id
                                         where l.LinkCategoryID == c.LinkCategoryID
                                         select new PageLinkVM
                                         {
                                             LinkID = l.LinkID,
                                             Title = l.Title,
                                             RoleName = r.Name,
                                             IsAssign = (from a in Context.PageLinkAssigns where a.LinkID == l.LinkID && a.RegistrationID == RegID select a.LinkAssignID).Count() > 0
                                         }).ToList()
                            }).ToList();
            return user_dll;
        }



    }
}
