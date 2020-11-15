namespace ORDER_MANAGEMENT.Data
{
    public interface IDepotStockRepository : IRepository<DepotStock>
    {
        void AddQuantity(DepotStockAdd model);
        DataResult<DepotProductViewModel> ProductsDataTable(DataRequest request, int[] filer);
    }


}