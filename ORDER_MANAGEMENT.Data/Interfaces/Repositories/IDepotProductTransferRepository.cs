using System;

namespace ORDER_MANAGEMENT.Data
{
    public interface IDepotProductTransferRepository : IRepository<DepotProductTransfer>
    {
        DataResult<DepotProductTransferViewModel> ListDataTable(DataRequest request);
    }


}