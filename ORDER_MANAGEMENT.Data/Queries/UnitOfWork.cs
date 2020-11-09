using System.Threading.Tasks;

namespace ORDER_MANAGEMENT.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;

            Areas = new AreaRepository(_context);
            Equipments = new EquipmentRepository(_context);
            EquipmentTypes = new EquipmentTypeRepository(_context);
            EquipmentDistributions = new EquipmentDistributionRepository(_context);
            Distributors = new DistributorRepository(_context);
            DistributorOrders = new DistributorOrderRepository(_context);
            DistributorOrderChanges = new DistributorOrderChangeRepository(_context);
            DistributorOrderLists = new DistributorOrderListRepository(_context);
            DistributorPaymentRecords = new DistributorPaymentRecordRepository(_context);
            DistributorProductDamages = new DistributorProductDamageRepository(_context);
            DistributorProductReturns = new DistributorProductReturnRepository(_context);
            DistributorStocks = new DistributorStockRepository(_context);
            Hierarchys = new HierarchyRepository(_context);
            Outlets = new OutletRepository(_context);
            OutletOrders = new OutletOrderRepository(_context);
            OutletProductReturns = new OutletProductReturnRepository(_context);
            OutletPaymentRecords = new OutletPaymentRecordRepository(_context);
            PageLinks = new PageLinkRepository(_context);
            PageLinkCategorys = new PageLinkCategoryRepository(_context);
            PageLinkAssigns = new PageLinkAssignRepository(_context);
            Products = new ProductRepository(_context);
            ProductCategorys = new ProductCategoryRepository(_context);
            ProductMainCategorys = new ProductMainCategoryRepository(_context);
            Regions = new RegionRepository(_context);
            Registrations = new RegistrationRepository(_context);
            Routes = new RouteRepository(_context);
            RouteDays = new RouteDaysRepository(_context);
            Territorys = new TerritoryRepository(_context);
            Targets = new TargetRepository(_context);
            TargetAssigns = new TargetAssignRepository(_context);
            Users = new UserRepository(_context);
            UserTrackingByDistributors = new UserTrackingByDistributorRepository(_context);
            UserTrackingByOutlets = new UserTrackingByOutletRepository(_context);
            UserRoutes = new UserRouteRepository(_context);
        }

        public IAreaRepository Areas { get; private set; }
        public IEquipmentRepository Equipments { get; }
        public IEquipmentTypeRepository EquipmentTypes { get; }
        public IEquipmentDistributionRepository EquipmentDistributions { get; }
        public IDistributorRepository Distributors { get; private set; }

        public IDistributorOrderRepository DistributorOrders { get; private set; }

        public IDistributorOrderChangeRepository DistributorOrderChanges { get; private set; }

        public IDistributorOrderListRepository DistributorOrderLists { get; private set; }

        public IDistributorPaymentRecordRepository DistributorPaymentRecords { get; private set; }

        public IDistributorProductDamageRepository DistributorProductDamages { get; private set; }

        public IDistributorProductReturnRepository DistributorProductReturns { get; private set; }

        public IDistributorStockRepository DistributorStocks { get; private set; }
        public IHierarchyRepository Hierarchys { get; private set; }
        public IOutletRepository Outlets { get; private set; }
        public IOutletOrderRepository OutletOrders { get; private set; }
        public IOutletProductReturnRepository OutletProductReturns { get; private set; }
        public IOutletPaymentRecordRepository OutletPaymentRecords { get; private set; }
        public IPageLinkRepository PageLinks { get; private set; }
        public IPageLinkCategoryRepositoryRepository PageLinkCategorys { get; private set; }
        public IPageLinkAssignRepository PageLinkAssigns { get; private set; }
        public IProductRepository Products { get; private set; }
        public IProductCategoryRepository ProductCategorys { get; private set; }
        public IProductMainCategoryRepository ProductMainCategorys { get; }
        public IRegionRepository Regions { get; private set; }
        public IRouteRepository Routes { get; private set; }
        public IRouteDaysRepository RouteDays { get; private set; }
        public ITargetRepository Targets { get; private set; }
        public IRegistrationRepository Registrations { get; private set; }
        public ITerritoryRepository Territorys { get; private set; }
        public ITargetAssignRepository TargetAssigns { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserTrackingByDistributorRepository UserTrackingByDistributors { get; private set; }
        public IUserTrackingByOutletRepository UserTrackingByOutlets { get; private set; }
        public IUserRouteRepository UserRoutes { get; private set; }



        public int SaveChanges()
        {
            //try
            //{
            return _context.SaveChanges();
            //}
            //catch
            //{
            //    return 0;
            //}
        }
        public Task<int> SaveChangesAsync()
        {
            //try
            //{
            return _context.SaveChangesAsync();
            //}
            //catch
            //{
            //  return Task.FromResult(0);
            //}
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}