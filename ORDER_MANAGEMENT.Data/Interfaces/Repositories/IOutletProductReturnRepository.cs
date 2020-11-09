using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IOutletProductReturnRepository : IRepository<OutletProductReturn>
    {
        void ApprovedReturn(List<OutletProductReturn> model, int RegID);
    }
}
