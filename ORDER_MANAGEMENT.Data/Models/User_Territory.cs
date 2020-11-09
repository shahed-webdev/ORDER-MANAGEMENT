namespace ORDER_MANAGEMENT.Data
{
    public class User_Territory
    {
        public int RegistrationID { get; set; }
        public User User { get; set; }
        public int TerritoryID { get; set; }
        public Territory Territory { get; set; }
    }
}