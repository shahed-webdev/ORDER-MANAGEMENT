using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface ITargetRepository : IRepository<Target>
    {
        void CreateTarget(TargetVM targetVM);
        List<Target_ddl> TargetAssignddl(int id);
        GetTargetStatus ddl_UserTargetAmount(int TargetID, int RegistrationID, int LoginRegistrationID);
    }
}
