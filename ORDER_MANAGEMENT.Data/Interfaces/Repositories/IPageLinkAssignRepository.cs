using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IPageLinkAssignRepository : IRepository<PageLinkAssign>
    {
        ICollection<PageCategoryVM> SubAdminLinks(int RegID);

        string AssignLink(int RegID, ICollection<PageAssignVM> Links);
    }
}
