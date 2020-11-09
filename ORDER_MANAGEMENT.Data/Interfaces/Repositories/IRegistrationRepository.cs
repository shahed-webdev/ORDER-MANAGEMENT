using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IRegistrationRepository : IRepository<Registration>
    {
        int GetRegID_ByUserName(string UserName);
        string GetRankName_ByUserName(string UserName);
        AdminBasic GetAdminBasic(string UserName);
        AdminInfo GetAdminInfo(string UserName);
        Registration Reg_Update(string UserName, UserDetails reg);

        ICollection<DDL> SubAdmins();
        ICollection<AdminInfo> GetSubAdminList();
    }
}
