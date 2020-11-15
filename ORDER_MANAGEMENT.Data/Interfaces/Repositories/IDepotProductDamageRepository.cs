namespace ORDER_MANAGEMENT.Data
{
    public interface IDepotProductDamageRepository : IRepository<DepotProductDamage>
    {
        void AddQuantity(DepotChangeQuantityModel model);
        DataResult<DepotProductDamageViewModel> ListDataTable(DataRequest request);
    }


}