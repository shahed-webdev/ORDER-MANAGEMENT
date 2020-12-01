using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public interface IUserRepository : IRepository<User>
    {
        List<UserddlVM> GetUsersDLLByHierarchy(int Rank, int RegistrationID = 0);

        void CreateUser(CreateUserVM userVM);
        UpdateUserVM UpdateDetails(int id);
        UserDashboardVM GetDashUser(int id);

        Registration UserInfoUpdate(int RegID, UserInfoUpdate userInfo);
        UserReportTo GetReportTo(int id);
        UserTargetReport GetTargetReport(int id);


        ICollection<UserReportFrom> GetReportFrom(int id);
        Registration ChangeHierarchyTerritory(UpdateUserVM model);
        List<UserVM> GetAllUser();
        List<DDL> GetUserDDL();
        List<DDL> GetUserByRankDdl(int rank);
        List<int> SubUserIdsByUser(int registrationId);
        List<UserSR> GetSR_ByDistributorTerritory(int DistributorID);
        UserVM GetUserInfo(int id);
        bool Is_Default_User(int id);

        //List<Area> GetUserArea(int RegistrationID);
        //List<Territory> GetUserTerritory(int RegistrationID);

        TargetInfo API_TargetInfo(int id);
        UserNameRank API_getUserInfo(int id);
        UserDetails GetUserDetails(string UserName);

        ICollection<OutletOrderReportModel> OrderReport(UserReportFilterModel filterModel);
        ICollection<OutletOrderReportModel> SalesReport(UserReportFilterModel filterModel);
        ICollection<OutletRevenueReportModelModel> RevenueReport(UserReportFilterModel filterModel);

    }


}
