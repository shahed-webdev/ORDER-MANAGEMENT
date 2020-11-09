using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IOutletPaymentRecordRepository : IRepository<OutletPaymentRecord>
    {
        List<OutletDueList> OutletDueList();

        void PayDue(OutletPaymentRecord model);
    }
}
