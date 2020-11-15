using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductReturnRepository : Repository<DepotProductReturn>, IDepotProductReturnRepository
    {
        public DepotProductReturnRepository(DataContext context) : base(context)
        {
        }

        public void AddQuantity(DepotReturnAddModel model)
        {
            var product = Context.Products.Find(model.ProductID);

            var depotStock = Context.DepotStocks.FirstOrDefault(d => d.DepotId == model.DepotId && d.ProductID == model.ProductID);

            if (depotStock.Quantity < model.Quantity) return;



            depotStock.Quantity -= model.Quantity;
            depotStock.TotalReturn += model.Quantity;
            Context.Entry(depotStock).State = EntityState.Modified;


            var depotReturn = new DepotProductReturn
            {
                DepotId = model.DepotId,
                ProductID = model.ProductID,
                ReturnByRegistrationID = model.RegistrationID,
                Quantity = model.Quantity
            };
            Context.DepotProductReturns.Add(depotReturn);

            product.Quantity += model.Quantity;
            Context.Entry(product).State = EntityState.Modified;
        }

        public DataResult<DepotProductReturnViewModel> ListDataTable(DataRequest request)
        {
            var query = Context.DepotProductReturns.Select(d => new DepotProductReturnViewModel
            {
                DepotProductReturnId = d.DepotProductReturnId,
                DepotId = d.DepotId,
                DepotName = d.Depot.DepotName,
                ProductID = d.ProductID,
                ProductName = d.Product.ProductName,
                ProductCode = d.Product.ProductCode,
                ReturnBy = d.ReturnByRegistration.Name,
                Quantity = d.Quantity,
                InsertDate = d.InsertDate
            }).OrderByDescending(d => d.InsertDate);

            return query.ToDataResult(request);
        }
    }
}