using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DBConnection")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<ChangeLog> ChangeLogs { get; set; }
        public virtual DbSet<Depot> Depots { get; set; }
        public virtual DbSet<DepotProductDamage> DepotProductDamages { get; set; }
        public virtual DbSet<DepotProductReturn> DepotProductReturns { get; set; }
        public virtual DbSet<DepotProductTransfer> DepotProductTransfers { get; set; }
        public virtual DbSet<DepotStock> DepotStocks { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; }
        public virtual DbSet<EquipmentDistribution> EquipmentDistributions { get; set; }
        public virtual DbSet<Distributor> Distributors { get; set; }
        public virtual DbSet<DistributorOrder> DistributorOrders { get; set; }
        public virtual DbSet<DistributorOrderChange> DistributorOrderChanges { get; set; }
        public virtual DbSet<DistributorOrderList> DistributorOrderLists { get; set; }
        public virtual DbSet<DistributorPaymentRecord> DistributorPaymentRecords { get; set; }
        public virtual DbSet<DistributorProductDamage> DistributorProductDamages { get; set; }
        public virtual DbSet<DistributorProductReturn> DistributorProductReturns { get; set; }
        public virtual DbSet<DistributorStock> DistributorStocks { get; set; }
        public virtual DbSet<Organization_hierarchy> Organization_hierarchy { get; set; }

        public virtual DbSet<Outlet> Outlets { get; set; }
        public virtual DbSet<Outlet_Images> Outlet_Images { get; set; }
        public virtual DbSet<OutletOrder> OutletOrders { get; set; }
        public virtual DbSet<OutletOrderChange> OutletOrderChanges { get; set; }
        public virtual DbSet<OutletOrderList> OutletOrderLists { get; set; }
        public virtual DbSet<OutletPaymentRecord> OutletPaymentRecords { get; set; }
        public virtual DbSet<OutletProductDamage> OutletProductDamages { get; set; }
        public virtual DbSet<OutletProductReturn> OutletProductReturns { get; set; }

        public virtual DbSet<PageLink> PageLinks { get; set; }
        public virtual DbSet<PageLinkCategory> PageLinkCategories { get; set; }
        public virtual DbSet<PageLinkAssign> PageLinkAssigns { get; set; }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductMainCategory> ProductMainCategories { get; set; }
        public virtual DbSet<ProductQuantityRecord> ProductQuantityRecords { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteDays> RouteDays { get; set; }

        public virtual DbSet<Target> Targets { get; set; }
        public virtual DbSet<TargetAssign> TargetAssigns { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserRoute> UserRoutes { get; set; }
        public virtual DbSet<User_Territory> User_Territories { get; set; }
        public virtual DbSet<UserTrackingByDistributor> UserTrackingByDistributors { get; set; }
        public virtual DbSet<UserTrackingByOutlet> UserTrackingByOutlets { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AreaConfiguration());
            modelBuilder.Configurations.Add(new AspNetRolesConfiguration());
            modelBuilder.Configurations.Add(new ChangeLogConfiguration());
            //modelBuilder.Configurations.Add(new DepotConfiguration());
            modelBuilder.Configurations.Add(new EquipmentConfiguration());
            modelBuilder.Configurations.Add(new EquipmentTypeConfiguration());
            modelBuilder.Configurations.Add(new EquipmentDistributionConfiguration());

            modelBuilder.Configurations.Add(new DistributorConfiguration());

            modelBuilder.Configurations.Add(new DistributorOrderConfiguration());
            modelBuilder.Configurations.Add(new DistributorOrderChangeConfiguration());
            modelBuilder.Configurations.Add(new DistributorOrderListConfiguration());
            modelBuilder.Configurations.Add(new DistributorPaymentRecordConfiguration());
            modelBuilder.Configurations.Add(new DistributorProductDamageConfiguration());
            modelBuilder.Configurations.Add(new DistributorProductReturnConfiguration());
            modelBuilder.Configurations.Add(new DistributorStockConfiguration());

            modelBuilder.Configurations.Add(new HierarchyConfiguration());

            modelBuilder.Configurations.Add(new OutletConfiguration());
            modelBuilder.Configurations.Add(new Outlet_ImagesConfiguration());
            modelBuilder.Configurations.Add(new OutletOrderConfiguration());
            modelBuilder.Configurations.Add(new OutletOrderChangeConfiguration());
            modelBuilder.Configurations.Add(new OutletOrderListConfiguration());
            modelBuilder.Configurations.Add(new OutletPaymentRecordConfiguration());
            modelBuilder.Configurations.Add(new OutletProductDamageConfiguration());
            modelBuilder.Configurations.Add(new OutletProductReturnConfiguration());

            modelBuilder.Configurations.Add(new PageLinkConfiguration());
            modelBuilder.Configurations.Add(new PageLinkAssignConfiguration());
            modelBuilder.Configurations.Add(new PageLinkCategoryConfiguration());

            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductMainCategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductQuantityRecordConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new RegistrationConfiguration());
            modelBuilder.Configurations.Add(new RouteConfiguration());
            modelBuilder.Configurations.Add(new RouteDaysConfiguration());
            modelBuilder.Configurations.Add(new TargetConfiguration());
            modelBuilder.Configurations.Add(new TargetAssignConfiguration());
            modelBuilder.Configurations.Add(new TerritoryConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new User_TerritoryConfiguration());
            modelBuilder.Configurations.Add(new UserTrackingByDistributorConfiguration());
            modelBuilder.Configurations.Add(new UserTrackingByOutletConfiguration());
            modelBuilder.Configurations.Add(new UserRouteConfiguration());
        }


        //for log Record of update database
        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified).ToList();
            var now = DateTime.UtcNow;

            foreach (var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);

                foreach (var prop in change.OriginalValues.PropertyNames)
                {
                    //var originalValue = change.OriginalValues[prop].ToString();
                    //var currentValue = change.CurrentValues[prop].ToString();
                    //if (originalValue != currentValue)


                    var originalValue = Convert.ToString(change.GetDatabaseValues().GetValue<object>(prop));
                    var currentValue = Convert.ToString(change.CurrentValues[prop]);
                    //If You to set any Default value when original or current is NULL then use this
                    if (originalValue == null || originalValue.ToString().Trim() == "")
                    { originalValue = "BLANK"; }

                    if (currentValue == null || currentValue.ToString().Trim() == "")
                    { currentValue = "BLANK"; }

                    if (!originalValue.Equals(currentValue)) //Only create a log if the value changes
                    {
                        ChangeLog log = new ChangeLog()
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = now
                        };
                        ChangeLogs.Add(log);
                    }
                }
            }
            return base.SaveChanges();
        }
        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}