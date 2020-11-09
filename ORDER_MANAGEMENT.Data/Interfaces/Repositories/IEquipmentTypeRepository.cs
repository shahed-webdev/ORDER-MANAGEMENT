using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IEquipmentTypeRepository : IRepository<EquipmentType>
    {
        ICollection<DDL> GetDdl();
        DataResult<EquipmentType> DT(DataRequest request);
    }
}
