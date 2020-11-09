using System.ComponentModel.DataAnnotations.Schema;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorOrderList
    {
        public int DistributorOrderListID { get; set; }
        public int DistributorOrderID { get; set; }
        public int ProductID { get; set; }
        public int OrderQuantity { get; set; }
        public int ReturnQuantity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int NetQuantity { get; set; }
        public double UnitPrice { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double LineTotal { get; set; }
        public virtual DistributorOrder DistributorOrder { get; set; }
        public virtual Product Product { get; set; }

    }
}
