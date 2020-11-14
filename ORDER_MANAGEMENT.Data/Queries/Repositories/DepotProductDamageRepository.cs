namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductDamageRepository : Repository<DepotProductDamage>, IDepotProductDamageRepository
    {
        public DepotProductDamageRepository(DataContext context) : base(context)
        {
        }
    }
}