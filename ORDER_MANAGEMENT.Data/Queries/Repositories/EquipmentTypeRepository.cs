using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class EquipmentTypeRepository : Repository<EquipmentType>, IEquipmentTypeRepository
    {
        public EquipmentTypeRepository(DataContext context) : base(context)
        {

        }

        public ICollection<DDL> GetDdl()
        {
            return GetAll().Select(e => new DDL { label = e.EquipmentTypeName, value = e.EquipmentTypeID }).ToList();
        }

        public DataResult<EquipmentType> DT(DataRequest request)
        {
            return Context.EquipmentTypes.ToDataResult(request);
        }
    }
}