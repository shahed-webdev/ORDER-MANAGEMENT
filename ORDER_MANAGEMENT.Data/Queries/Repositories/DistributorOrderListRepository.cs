namespace ORDER_MANAGEMENT.Data
{
    public class DistributorOrderListRepository : Repository<DistributorOrderList>, IDistributorOrderListRepository
    {
        public DistributorOrderListRepository(DataContext context) : base(context)
        {

        }
    }
}
