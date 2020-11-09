using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IEquipmentRepository : IRepository<Equipment>
    {
        ICollection<EquipmentVM> Search(string key);
        DataResult<EquipmentStatsVM> DT_TypeWise(DataRequest request, int[] filer);
        EquipmentDetails Details(int id);
    }
}
