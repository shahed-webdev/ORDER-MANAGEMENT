namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductReturnRepository : Repository<DepotProductReturn>, IDepotProductReturnRepository
    {
        public DepotProductReturnRepository(DataContext context) : base(context)
        {
        }
    }
}