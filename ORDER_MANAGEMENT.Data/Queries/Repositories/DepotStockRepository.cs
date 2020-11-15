using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotStockRepository : Repository<DepotStock>, IDepotStockRepository
    {
        public DepotStockRepository(DataContext context) : base(context)
        {
        }

        public void AddQuantity(DepotStockAdd model)
        {
            var product = Context.Products.Find(model.ProductID);

            if (product.Quantity < model.Quantity) return;

            var depotStock = Context.DepotStocks
                .FirstOrDefault(d => d.DepotId == model.DepotId && d.ProductID == model.ProductID);

            if (depotStock == null)
            {
                depotStock = new DepotStock
                {
                    DepotId = model.DepotId,
                    ProductID = model.ProductID,
                    Quantity = model.Quantity,
                };
                Context.DepotStocks.Add(depotStock);
            }
            else
            {
                depotStock.Quantity += model.Quantity;
                Context.Entry(depotStock).State = EntityState.Modified;
            }

            var depotTransfer = new DepotProductTransfer
            {
                DepotId = model.DepotId,
                ProductID = model.ProductID,
                TransferByRegistrationID = model.RegistrationID,
                Quantity = model.Quantity
            };
            Context.DepotProductTransfers.Add(depotTransfer);

            Context.Entry(product).State = EntityState.Modified;

        }
    }
}