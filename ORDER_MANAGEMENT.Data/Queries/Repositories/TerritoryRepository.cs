using System.Collections.Generic;
using System.Linq;

namespace ORDER_MANAGEMENT.Data
{
    public class TerritoryRepository : Repository<Territory>, ITerritoryRepository
    {
        public TerritoryRepository(DataContext context) : base(context)
        {

        }

        public void DistributorPercentageChange(int TerritoryID, double Percentage)
        {
            var territory = Find(TerritoryID);
            territory.DistributorDiscountPercentage = Percentage;
            Update(territory);
        }

        public List<Territory> GetTerritory(List<int> AreaIDs)
        {
            var Territorys = new List<Territory>();
            Territorys = Where(t => AreaIDs.Contains(t.AreaID)).ToList();

            return Territorys;
        }
        public List<TerritoryDll_VM> GetUserTerritory(int UpperRegistrationID, List<int> AreaIDs)
        {
            var user = Context.Users.Find(UpperRegistrationID);
            var Territorys = new List<Territory>();

            if (user.IsDefaultUser)
            {
                Territorys = GetAll().ToList();
            }
            else
            {
                Territorys = Context.User_Territories.Where(u => u.RegistrationID == UpperRegistrationID).Select(t => t.Territory).ToList();
            }
            return Territorys.Where(t => AreaIDs.Contains(t.AreaID)).Select(t => new TerritoryDll_VM() { TerritoryID = t.TerritoryID, TerritoryName = t.TerritoryName }).ToList();
        }

        public List<DDL> GetUserTerritory(int RegistrationID)
        {
            var user = Context.Users.Find(RegistrationID);
            var Territorys = new List<Territory>();

            if (user.IsDefaultUser)
            {
                Territorys = GetAll().ToList();
            }
            else
            {
                Territorys = Context.User_Territories.Where(u => u.RegistrationID == RegistrationID).Select(t => t.Territory).ToList();
            }
            return Territorys.Select(t => new DDL() { value = t.TerritoryID, label = t.TerritoryName }).ToList();
        }

        public List<TerritoryDll_VM> GetUserUnAsignTerritory(int UpperRegistrationID, List<int> AreaIDs, int RegistrationID = 0)
        {
            var UpperUser = Context.Users.Find(UpperRegistrationID);
            var Territorys = new List<Territory>();

            if (UpperUser.IsDefaultUser)
            {
                Territorys = GetAll().ToList();
            }
            else
            {
                Territorys = Context.User_Territories.Where(u => u.RegistrationID == UpperRegistrationID).Select(t => t.Territory).ToList();


                var user_Assing_TerritoryIDs = (from ta in Context.User_Territories
                                                where ta.User.Upper_RegistrationID == UpperUser.RegistrationID && ta.RegistrationID != RegistrationID
                                                select (ta.TerritoryID)).ToList();


                Territorys = Territorys.Where(t => !user_Assing_TerritoryIDs.Contains(t.TerritoryID)).ToList();

            }
            return Territorys.Where(t => AreaIDs.Contains(t.AreaID)).Select(t => new TerritoryDll_VM() { TerritoryID = t.TerritoryID, TerritoryName = t.TerritoryName }).ToList();
        }
    }
}