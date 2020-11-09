namespace ORDER_MANAGEMENT.Data
{
    public class Outlet_ImagesRepository : Repository<Outlet_Images>, IOutlet_ImagesRepository
    {
        public Outlet_ImagesRepository(DataContext context) : base(context)
        {

        }
    }
}
