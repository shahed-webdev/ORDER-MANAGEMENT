using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(DataContext context) : base(context)
        {
        }
        public ICollection<EquipmentVM> Search(string key)
        {
            var data = Context.Equipments.Include(e => e.EquipmentType)
                .Where(e => e.Code.Contains(key) && !e.EquipmentDistributions.Any()).Select(e =>
                   new EquipmentVM
                   {
                       EquipmentID = e.EquipmentID,
                       EquipmentTypeID = e.EquipmentTypeID,
                       EquipmentName = e.EquipmentName,
                       EquipmentTypeName = e.EquipmentType.EquipmentTypeName,
                       Code = e.Code,
                       Size = e.Size
                   });
            return data.Take(5).ToList();
        }
        public DataResult<EquipmentStatsVM> DT_TypeWise(DataRequest request, int[] filer)
        {
            var query = (from e in Context.Equipments
                         join t in Context.EquipmentTypes on e.EquipmentTypeID equals t.EquipmentTypeID
                         join d in Context.EquipmentDistributions on e.EquipmentID equals d.EquipmentID into ed
                         from d in ed.Where(dis => dis.IsCurrent).DefaultIfEmpty()
                         join o in Context.Outlets on d.OutletID equals o.OutletID into eo
                         from o in eo.DefaultIfEmpty()
                         where filer.Contains(e.EquipmentTypeID)
                         select new EquipmentStatsVM
                         {
                             EquipmentID = e.EquipmentID,
                             EquipmentTypeID = e.EquipmentTypeID,
                             EquipmentName = e.EquipmentName,
                             EquipmentTypeName = t.EquipmentTypeName,
                             Code = e.Code,
                             Size = e.Size,
                             IsDistributed = d.EquipmentDistributionID != null,
                             OutletName = o.OutletName,
                             ProprietorName = o.ProprietorName,
                             Phone = o.Phone,
                             Address = o.Address
                         });

            return query.ToDataResult(request);
        }

        public EquipmentDetails Details(int id)
        {
            var details = Context.Equipments.Include(e => e.EquipmentType).Select(e => new EquipmentDetails
            {
                EquipmentID = e.EquipmentID,
                EquipmentTypeID = e.EquipmentTypeID,
                EquipmentName = e.EquipmentName,
                EquipmentTypeName = e.EquipmentType.EquipmentTypeName,
                Code = e.Code,
                Size = e.Size

            }).FirstOrDefault(e => e.EquipmentID == id);


            details.CurrentOutlet = (from e in Context.Equipments
                                     join t in Context.EquipmentTypes on e.EquipmentTypeID equals t.EquipmentTypeID
                                     join d in Context.EquipmentDistributions on e.EquipmentID equals d.EquipmentID
                                     join o in Context.Outlets on d.OutletID equals o.OutletID
                                     join ta in Context.Territories on o.TerritoryID equals ta.TerritoryID
                                     join r in Context.Registrations on d.AssignByRegistrationID equals r.RegistrationID
                                     where e.EquipmentID == id && d.IsCurrent
                                     select new EquipmentDistributionDetails
                                     {
                                         EquipmentDistributionID = d.EquipmentDistributionID,
                                         OutletInfo = new OutletSearch
                                         {
                                             OutletID = o.OutletID,
                                             OutletName = o.OutletName,
                                             ProprietorName = o.ProprietorName,
                                             Phone = o.Phone,
                                             Address = o.Address,
                                             RegionName = null,
                                             AreaName = null,
                                             TerritoryName = ta.TerritoryName
                                         },
                                         Location = d.Location,
                                         InstalledDate = d.InstalledDate,
                                         UninstalledDate = d.UninstalledDate,
                                         TechnicianName = d.TechnicianName,
                                         InChargeName = d.InChargeName,
                                         Value = d.Value,
                                         RentStatus = d.RentStatus,
                                         RentPrice = d.RentPrice,
                                         RentInterval = d.RentInterval,
                                         AssignBy = r.Name
                                     }).FirstOrDefault();

            details.DistributionDetails = (from e in Context.Equipments
                                           join t in Context.EquipmentTypes on e.EquipmentTypeID equals t.EquipmentTypeID
                                           join d in Context.EquipmentDistributions on e.EquipmentID equals d.EquipmentID
                                           join o in Context.Outlets on d.OutletID equals o.OutletID
                                           join ta in Context.Territories on o.TerritoryID equals ta.TerritoryID
                                           join r in Context.Registrations on d.AssignByRegistrationID equals r.RegistrationID
                                           where e.EquipmentID == id && !d.IsCurrent
                                           select new EquipmentDistributionDetails
                                           {
                                               EquipmentDistributionID = d.EquipmentDistributionID,
                                               OutletInfo = new OutletSearch
                                               {
                                                   OutletID = o.OutletID,
                                                   OutletName = o.OutletName,
                                                   ProprietorName = o.ProprietorName,
                                                   Phone = o.Phone,
                                                   Address = o.Address,
                                                   RegionName = null,
                                                   AreaName = null,
                                                   TerritoryName = ta.TerritoryName
                                               },
                                               Location = d.Location,
                                               InstalledDate = d.InstalledDate,
                                               UninstalledDate = d.UninstalledDate,
                                               TechnicianName = d.TechnicianName,
                                               InChargeName = d.InChargeName,
                                               Value = d.Value,
                                               RentStatus = d.RentStatus,
                                               RentPrice = d.RentPrice,
                                               RentInterval = d.RentInterval,
                                               AssignBy = r.Name
                                           }).ToList();
            return details;
        }
    }
}