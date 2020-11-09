namespace ORDER_MANAGEMENT.Data
{
    public class DistributorProductDamageRepository : Repository<DistributorProductDamage>, IDistributorProductDamageRepository
    {
        public DistributorProductDamageRepository(DataContext context) : base(context)
        {

        }
    }
}
