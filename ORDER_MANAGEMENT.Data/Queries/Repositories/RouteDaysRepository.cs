namespace ORDER_MANAGEMENT.Data
{
    class RouteDaysRepository : Repository<RouteDays>, IRouteDaysRepository
    {
        public RouteDaysRepository(DataContext context) : base(context)
        {

        }
    }
}
