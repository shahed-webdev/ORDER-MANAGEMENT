using System.ComponentModel.DataAnnotations.Schema;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrderList
    {
        public int OutletOrderListID { get; set; }
        public int OutletOrderID { get; set; }
        public int ProductID { get; set; }
        public int OrderQuantity { get; set; }
        public int ReturnQuantity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int NetQuantity { get; set; }
        public double UnitPrice { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double LineTotal { get; set; }
        public virtual OutletOrder OutletOrder { get; set; }
        public virtual Product Product { get; set; }

    }
}
