using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IDistributorPaymentRecordRepository : IRepository<DistributorPaymentRecord>
    {
        List<DistributorDueList> DistributorDueListByUser(int id);

        void PayDue(DistributorPaymentRecord model);
    }
}
