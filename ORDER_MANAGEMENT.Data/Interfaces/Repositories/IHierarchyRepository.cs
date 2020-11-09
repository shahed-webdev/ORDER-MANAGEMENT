using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORDER_MANAGEMENT.Data
{
    public interface IHierarchyRepository : IRepository<Organization_hierarchy>
    {
        Task<DataTablesResponse> DataTable_DataAsync(IDataTablesRequest requestModel);

        List<HierarchyDll_VM> GetDll_Hierarchy();
    }
}