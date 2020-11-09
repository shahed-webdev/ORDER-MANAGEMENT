using System;
using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IPageLinkCategoryRepositoryRepository : IRepository<PageLinkCategory>
    {
        ICollection<PageLinkCategory> GetCategoryWithLink();

        PageLink LinkRoleUpdate(int LinkID, Guid RoleId);

        ICollection<DDL> ddl();
    }
}
