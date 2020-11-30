using System.Data.Entity.ModelConfiguration;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorTerritoryListConfiguration : EntityTypeConfiguration<DistributorTerritoryList>
    {
        public DistributorTerritoryListConfiguration()
        {
            ToTable("DistributorTerritoryList");
            HasKey(d => d.DistributorTerritoryListId);
            HasRequired(t => t.Territory).WithMany(t => t.DistributorTerritoryLists).HasForeignKey(t => t.TerritoryID);
            HasRequired(u => u.Distributor).WithMany(u => u.DistributorTerritoryLists).HasForeignKey(u => u.DistributorID);
        }
    }
}