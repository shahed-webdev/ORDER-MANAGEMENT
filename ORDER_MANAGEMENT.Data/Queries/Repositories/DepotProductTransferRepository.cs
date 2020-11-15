using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductTransferRepository : Repository<DepotProductTransfer>, IDepotProductTransferRepository
    {
        public DepotProductTransferRepository(DataContext context) : base(context)
        {
        }

        public DataResult<DepotProductTransferViewModel> ListDataTable(DataRequest request)
        {
            var query = Context.DepotProductTransfers.Select(d => new DepotProductTransferViewModel
            {
                DepotProductTransferId = d.DepotProductTransferId,
                DepotId = d.DepotId,
                DepotName = d.Depot.DepotName,
                ProductID = d.ProductID,
                ProductName = d.Product.ProductName,
                ProductCode = d.Product.ProductCode,
                TransferBy = d.TransferByRegistration.Name,
                Quantity = d.Quantity,
                InsertDate = d.InsertDate
            }).OrderByDescending(d => d.InsertDate);

            return query.ToDataResult(request);
        }
    }
}