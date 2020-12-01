using System.Collections.Generic;

namespace ORDER_MANAGEMENT.Data
{
    public static class UserHierarchyStatic
    {
        public static void CatalogIdsFunction(int registrationId, ICollection<User> subUsers, List<int> ids)
        {
            ids.Add(registrationId);

            foreach (var subUser in subUsers)
            {
                if (subUser.SubUsers != null)
                {
                    CatalogIdsFunction(subUser.RegistrationID, subUser.SubUsers, ids);
                }
            }
        }
    }
}