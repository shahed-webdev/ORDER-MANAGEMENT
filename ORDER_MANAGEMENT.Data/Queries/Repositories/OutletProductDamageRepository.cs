namespace ORDER_MANAGEMENT.Data
{
    public class OutletProductDamageRepository : Repository<OutletProductDamage>, IOutletProductDamageRepository
    {
        public OutletProductDamageRepository(DataContext context) : base(context)
        {

        }
    }
}
