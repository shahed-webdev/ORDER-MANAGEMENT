using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductDamageRepository : Repository<DepotProductDamage>, IDepotProductDamageRepository
    {
        public DepotProductDamageRepository(DataContext context) : base(context)
        {
        }

        public void AddQuantity(DepotChangeQuantityModel model)
        {
            var depotStock = Context.DepotStocks.FirstOrDefault(d => d.DepotId == model.DepotId && d.ProductID == model.ProductID);

            if (depotStock.Quantity < model.Quantity) return;



            depotStock.Quantity -= model.Quantity;
            depotStock.TotalDamage += model.Quantity;
            Context.Entry(depotStock).State = EntityState.Modified;


            var depotReturn = new DepotProductDamage
            {
                DepotId = model.DepotId,
                ProductID = model.ProductID,
                DamageByRegistrationID = model.RegistrationID,
                Quantity = model.Quantity
            };
            Context.DepotProductDamages.Add(depotReturn);
        }

        public DataResult<DepotProductDamageViewModel> ListDataTable(DataRequest request)
        {
            var query = Context.DepotProductDamages.Select(d => new DepotProductDamageViewModel
            {
                DepotProductDamageId = d.DepotProductDamageId,
                DepotId = d.DepotId,
                DepotName = d.Depot.DepotName,
                ProductID = d.ProductID,
                ProductName = d.Product.ProductName,
                ProductCode = d.Product.ProductCode,
                DamageBy = d.DamageByRegistration.Name,
                Quantity = d.Quantity,
                InsertDate = d.InsertDate
            }).OrderByDescending(d => d.InsertDate);

            return query.ToDataResult(request);
        }
    }
}