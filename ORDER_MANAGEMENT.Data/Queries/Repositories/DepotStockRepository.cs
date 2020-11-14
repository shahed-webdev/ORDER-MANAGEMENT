namespace ORDER_MANAGEMENT.Data
{
    public class DepotStockRepository : Repository<DepotStock>, IDepotStockRepository
    {
        public DepotStockRepository(DataContext context) : base(context)
        {
        }
    }
}