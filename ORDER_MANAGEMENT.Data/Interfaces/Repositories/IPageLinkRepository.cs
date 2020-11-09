using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IPageLinkRepository : IRepository<PageLink>
    {
        List<SideMenuCategory> GetSideMenuByUser(string UserName);
    }
}
