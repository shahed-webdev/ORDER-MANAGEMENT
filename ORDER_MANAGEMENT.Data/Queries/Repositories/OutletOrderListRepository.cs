namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderListRepository : Repository<OutletOrderList>, IOutletOrderListRepository
    {
        public OutletOrderListRepository(DataContext context) : base(context)
        {

        }
    }
}
