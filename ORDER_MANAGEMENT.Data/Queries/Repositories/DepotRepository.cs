namespace ORDER_MANAGEMENT.Data
{
    public class DepotRepository : Repository<Depot>, IDepotRepository
    {
        public DepotRepository(DataContext context) : base(context)
        {
        }
    }
}