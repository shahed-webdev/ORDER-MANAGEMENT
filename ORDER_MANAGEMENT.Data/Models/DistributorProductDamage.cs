using System;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorProductDamage
    {
        public int DistributorProductDamageID { get; set; }
        public int DistributorID { get; set; }
        public int ReportBy_RegistrationID { get; set; }
        public int ApproveBy_RegistrationID { get; set; }
        public int ProductID { get; set; }
        public int DamageQuantity { get; set; }
        public DateTime DamageDate { get; set; }
        public string Condition { get; set; }
        public byte[] Image { get; set; }
        public DateTime ApproveDate { get; set; }
        public bool Is_Approved { get; set; } = false;
        public DateTime InsertDate { get; set; } = DateTime.Now;


        public virtual Distributor Distributor { get; set; }
        public virtual Product Product { get; set; }
        public virtual Registration ReportBy_Registration { get; set; }
        public virtual Registration ApproveBy_Registration { get; set; }
    }
}
