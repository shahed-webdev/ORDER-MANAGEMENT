using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class TargetRepository : Repository<Target>, ITargetRepository
    {
        public TargetRepository(DataContext context) : base(context)
        {

        }

        public void CreateTarget(TargetVM targetVM)
        {
            var target = new Target()
            {
                EndDate = targetVM.EndDate,
                StartDate = targetVM.StartDate,
                CreatedByRegistrationID = targetVM.CreatedByRegistrationID,
                Target_Title = targetVM.Target_Title,
                Total_TargetAmount = targetVM.Total_TargetAmount
            };

            Add(target);
        }

        public GetTargetStatus ddl_UserTargetAmount(int TargetID, int RegistrationID, int LoginRegistrationID)
        {
            var Is_Default_User = Context.Users.Find(LoginRegistrationID).IsDefaultUser;


            if (Is_Default_User)
            {
                var t_status = (from t in Context.Targets
                                where t.TargetID == TargetID
                                select new GetTargetStatus
                                {
                                    Achieved = t.Total_AchievedAmount,
                                    Target = t.Total_TargetAmount,
                                    Assinged = t.TargetAssigns.Where(ta => ta.AssignByRegistrationID == LoginRegistrationID).Sum(ta => (double?)ta.TargetAmount) ?? 0
                                }).FirstOrDefault();

                t_status.Assignable = t_status.Target - t_status.Assinged;
                t_status.Is_New_Target = !Context.TargetAssigns.Any(t => t.TargetID == TargetID && t.RegistrationID == RegistrationID);

                if (!t_status.Is_New_Target)
                {
                    t_status.User_Prev_Target_Amount = (double?)Context.TargetAssigns.FirstOrDefault(t => t.TargetID == TargetID && t.RegistrationID == RegistrationID).TargetAmount ?? 0;
                }

                return t_status;
            }
            else
            {
                var t_status = (from t in Context.TargetAssigns
                                where t.TargetID == TargetID && t.RegistrationID == LoginRegistrationID
                                select new GetTargetStatus
                                {
                                    Achieved = t.AchievedAmount,
                                    Target = t.TargetAmount,
                                    Assinged = Context.TargetAssigns.Where(ta => ta.TargetID == TargetID && ta.AssignByRegistrationID == LoginRegistrationID).Sum(ta => (double?)ta.TargetAmount) ?? 0
                                }).FirstOrDefault();

                t_status.Assignable = t_status.Target - t_status.Assinged;
                t_status.Is_New_Target = !Context.TargetAssigns.Any(t => t.TargetID == TargetID && t.RegistrationID == RegistrationID);

                if (!t_status.Is_New_Target)
                {
                    t_status.User_Prev_Target_Amount = (double?)Context.TargetAssigns.FirstOrDefault(t => t.TargetID == TargetID && t.RegistrationID == RegistrationID).TargetAmount ?? 0;
                }

                return t_status;
            }
        }

        public List<Target_ddl> TargetAssignddl(int id)
        {
            var Is_Default_User = Context.Users.Find(id).IsDefaultUser;

            if (Is_Default_User)
            {
                var t_ddl = (from t in Context.Targets
                             where t.Total_TargetAmount - t.Total_AchievedAmount > 0
                             select new Target_ddl
                             {
                                 TargetID = t.TargetID,
                                 Target_Title = t.Target_Title
                             }).
                             ToList();

                return t_ddl;
            }
            else
            {
                var t_ddl = (from t in Context.TargetAssigns
                             where (t.TargetAmount - t.AchievedAmount) > 0 && t.RegistrationID == id
                             select new Target_ddl
                             {
                                 TargetID = t.TargetID,
                                 Target_Title = t.Target.Target_Title
                             }).
                             Distinct().ToList();

                return t_ddl;
            }
        }
    }
}