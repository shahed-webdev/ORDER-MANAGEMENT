namespace ORDER_MANAGEMENT.Data
{
    public class DistributorOrderChangeRepository : Repository<DistributorOrderChange>, IDistributorOrderChangeRepository
    {
        public DistributorOrderChangeRepository(DataContext context) : base(context)
        {

        }
    }
}
