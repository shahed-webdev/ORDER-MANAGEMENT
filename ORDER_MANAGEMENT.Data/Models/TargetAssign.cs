using System;

namespace ORDER_MANAGEMENT.Data
{
    public class TargetAssign
    {
        public int TargetAssignID { get; set; }
        public int TargetID { get; set; }
        public int RegistrationID { get; set; }
        public double TargetAmount { get; set; }
        public double AchievedAmount { get; set; } = 0;
        public int AssignByRegistrationID { get; set; }
        public DateTime AssignDate { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
        public virtual User AssignByUser { get; set; }
        public virtual Target Target { get; set; }
    }
}