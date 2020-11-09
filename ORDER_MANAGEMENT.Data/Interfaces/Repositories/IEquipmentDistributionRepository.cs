using System;

namespace ORDER_MANAGEMENT.Data
{
    public interface IEquipmentDistributionRepository : IRepository<EquipmentDistribution>
    {
        void DistributionRemove(int EquipmentId, DateTime uninstalledDate);
        void Distribution(EquipmentDistributionVM model);
    }
}