using System;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentDistributionRepository : Repository<EquipmentDistribution>, IEquipmentDistributionRepository
    {
        public EquipmentDistributionRepository(DataContext context) : base(context)
        {
        }

        public void DistributionRemove(int EquipmentId, DateTime uninstalledDate)
        {
            var ed = Context.EquipmentDistributions.FirstOrDefault(e => e.EquipmentID == EquipmentId && e.IsCurrent);
            ed.IsCurrent = false;
            ed.UninstalledDate = uninstalledDate;
            Update(ed);
        }

        public void Distribution(EquipmentDistributionVM model)
        {
            var ed = new EquipmentDistribution
            {
                EquipmentID = model.EquipmentID,
                OutletID = model.OutletID,
                Location = model.Location,
                InstalledDate = model.InstalledDate.GetValueOrDefault(),
                TechnicianName = model.TechnicianName,
                InChargeName = model.InChargeName,
                AssignByRegistrationID = model.AssignByRegistrationID,
                Value = model.Value,
                RentStatus = model.RentStatus,
                RentPrice = model.RentPrice,
                RentInterval = model.RentInterval
            };
            Add(ed);
        }
    }
}