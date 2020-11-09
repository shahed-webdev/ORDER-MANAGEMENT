using System;
using System.Threading.Tasks;

namespace ORDER_MANAGEMENT.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IAreaRepository Areas { get; }
        IEquipmentRepository Equipments { get; }
        IEquipmentTypeRepository EquipmentTypes { get; }
        IEquipmentDistributionRepository EquipmentDistributions { get; }
        IDistributorRepository Distributors { get; }
        IDistributorOrderRepository DistributorOrders { get; }
        IDistributorOrderChangeRepository DistributorOrderChanges { get; }
        IDistributorOrderListRepository DistributorOrderLists { get; }
        IDistributorPaymentRecordRepository DistributorPaymentRecords { get; }
        IDistributorProductDamageRepository DistributorProductDamages { get; }
        IDistributorProductReturnRepository DistributorProductReturns { get; }
        IDistributorStockRepository DistributorStocks { get; }
        IHierarchyRepository Hierarchys { get; }
        IOutletRepository Outlets { get; }
        IOutletOrderRepository OutletOrders { get; }
        IOutletProductReturnRepository OutletProductReturns { get; }
        IOutletPaymentRecordRepository OutletPaymentRecords { get; }

        IPageLinkRepository PageLinks { get; }
        IPageLinkCategoryRepositoryRepository PageLinkCategorys { get; }
        IPageLinkAssignRepository PageLinkAssigns { get; }
        IProductRepository Products { get; }
        IProductCategoryRepository ProductCategorys { get; }
        IProductMainCategoryRepository ProductMainCategorys { get; }
        IRegionRepository Regions { get; }
        IRegistrationRepository Registrations { get; }
        IRouteRepository Routes { get; }
        IRouteDaysRepository RouteDays { get; }
        ITargetRepository Targets { get; }
        ITargetAssignRepository TargetAssigns { get; }
        ITerritoryRepository Territorys { get; }
        IUserRepository Users { get; }
        IUserTrackingByDistributorRepository UserTrackingByDistributors { get; }
        IUserTrackingByOutletRepository UserTrackingByOutlets { get; }
        IUserRouteRepository UserRoutes { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}