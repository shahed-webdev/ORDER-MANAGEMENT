namespace ORDER_MANAGEMENT.Data
{
    public interface IDepotProductReturnRepository : IRepository<DepotProductReturn>
    {
        void AddQuantity(DepotReturnAddModel model);
        DataResult<DepotProductReturnViewModel> ListDataTable(DataRequest request);
    }


}