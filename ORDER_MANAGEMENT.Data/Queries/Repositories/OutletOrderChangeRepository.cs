namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderChangeRepository : Repository<OutletOrderChange>, IOutletOrderChangeRepository
    {
        public OutletOrderChangeRepository(DataContext context) : base(context)
        {

        }
    }
}
