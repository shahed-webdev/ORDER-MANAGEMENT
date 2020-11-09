using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORDER_MANAGEMENT.Data
{
    public class OutletOrder
    {

        public OutletOrder()
        {
            this.OutletOrderLists = new HashSet<OutletOrderList>();
            this.OutletOrderChanges = new HashSet<OutletOrderChange>();
        }
        public int OutletOrderID { get; set; }
        public int OutletOrder_SN { get; set; }
        public int OutletID { get; set; }
        public int DistributorID { get; set; }
        public int OrderBy_RegistrationID { get; set; }
        public int? ApproveBy_RegistrationID { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public DateTime? ApproveDate { get; set; }
        public double OrderTotalPrice { get; set; }
        public double OrderDiscount { get; set; }
        public double OrderReturnPrice { get; set; } = 0;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double OrderNetPrice { get; set; }
        public bool Is_Approved { get; set; } = false;
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual Outlet Outlet { get; set; }
        public virtual Distributor Distributor { get; set; }
        public virtual Registration OrderBy_Registration { get; set; }
        public virtual Registration ApproveBy_Registration { get; set; }
        public virtual ICollection<OutletOrderList> OutletOrderLists { get; set; }
        public virtual ICollection<OutletOrderChange> OutletOrderChanges { get; set; }

    }
}




