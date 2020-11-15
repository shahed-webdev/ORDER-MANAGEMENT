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

            product.Quantity -= model.Quantity;
            Context.Entry(product).State = EntityState.Modified;

        }

        public DataResult<DepotProductViewModel> ProductsDataTable(DataRequest request, int[] filer)
        {
            var query = Context.DepotStocks.Where(t => filer.Contains(t.Product.ProductCategoryID)).Select(d => new DepotProductViewModel
            {
                DepotStockId = d.DepotStockId,
                DepotId = d.DepotId,
                ProductID = d.ProductID,
                ProductCategoryID = d.Product.ProductCategoryID,
                ProductName = d.Product.ProductName,
                ProductCode = d.Product.ProductCode,
                SKU = d.Product.SKU,
                Size = d.Product.Size,
                MRP = d.Product.MRP,
                Quantity = d.Quantity
            }).OrderBy(d => d.ProductName);
            return query.ToDataResult(request);
        }
    }
}