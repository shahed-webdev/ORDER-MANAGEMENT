namespace ORDER_MANAGEMENT.Data
{
    public class DepotProductTransferRepository : Repository<DepotProductTransfer>, IDepotProductTransferRepository
    {
        public DepotProductTransferRepository(DataContext context) : base(context)
        {
        }
    }
}