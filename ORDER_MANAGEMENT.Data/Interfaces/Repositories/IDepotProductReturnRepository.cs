namespace ORDER_MANAGEMENT.Data
{
    public interface IDepotProductReturnRepository : IRepository<DepotProductReturn>
    {
        void AddQuantity(DepotChangeQuantityModel model);
        DataResult<DepotProductReturnViewModel> ListDataTable(DataRequest request);
    }


}