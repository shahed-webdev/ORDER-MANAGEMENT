using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ORDER_MANAGEMENT.Data
{
    public class HierarchyRepository : Repository<Organization_hierarchy>, IHierarchyRepository
    {
        public HierarchyRepository(DataContext context) : base(context)
        {

        }


        public async Task<DataTablesResponse> DataTable_DataAsync(IDataTablesRequest request)
        {
            var data = await GetAllAsync();
            var filteredData = data.Where(_item => _item.HierarchyName.Contains(request.Search.Value));
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return response;
        }

        public List<HierarchyDll_VM> GetDll_Hierarchy()
        {
            var Hierarchys = new List<HierarchyDll_VM>();
            Hierarchys = GetAll().Where(h => h.Rank > 1).Select(n => new HierarchyDll_VM() { Rank = n.Rank, HierarchyName = n.Rank + ". " + n.HierarchyName }).OrderBy(h => h.Rank).ToList();

            return Hierarchys;
        }

        public ICollection<RankWiseUser> RankWiseNumberOfUser()
        {
            var list = Context.Users.Include(u => u.Organization_hierarchy).GroupBy(u => u.Organization_hierarchy)
                .Select(g => new RankWiseUser
                {
                    Rank = g.Key.Rank,
                    HierarchyName = g.Key.HierarchyName,
                    UserCount = g.Count()
                }).ToList();

            return list;
        }
    }
}