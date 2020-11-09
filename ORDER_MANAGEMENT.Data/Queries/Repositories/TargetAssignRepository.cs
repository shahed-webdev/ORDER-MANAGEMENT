namespace ORDER_MANAGEMENT.Data
{
    public class TargetAssignRepository : Repository<TargetAssign>, ITargetAssignRepository
    {
        public TargetAssignRepository(DataContext context) : base(context)
        {

        }
    }
}