namespace ORDER_MANAGEMENT.Data
{
    public class DistributorStockRepository : Repository<DistributorStock>, IDistributorStockRepository
    {
        public DistributorStockRepository(DataContext context) : base(context)
        {

        }

    }
}
