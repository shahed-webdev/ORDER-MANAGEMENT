using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORDER_MANAGEMENT.Data
{
    public class Product
    {
        public Product()
        {


            this.ProductQuantityRecords = new HashSet<ProductQuantityRecord>();
            this.DistributorStocks = new HashSet<DistributorStock>();
            this.DistributorOrderLists = new HashSet<DistributorOrderList>();
            this.DistributorProductDamages = new HashSet<DistributorProductDamage>();
            this.DistributorOrderChanges = new HashSet<DistributorOrderChange>();

            this.OutletOrderLists = new HashSet<OutletOrderList>();
            this.OutletProductDamages = new HashSet<OutletProductDamage>();
            this.OutletOrderChanges = new HashSet<OutletOrderChange>();


        }
        public int ProductID { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int SKU { get; set; }
        public string Size { get; set; }
        public double? MRP { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public byte[] ProductImage { get; set; }
        public DateTime? InsertDate { get; set; } = DateTime.Now;
        public virtual ProductCategory ProductCategory { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double TotalPrice { get; private set; }

        public virtual ICollection<ProductQuantityRecord> ProductQuantityRecords { get; set; }
        public virtual ICollection<DistributorStock> DistributorStocks { get; set; }
        public virtual ICollection<DistributorOrderList> DistributorOrderLists { get; set; }
        public virtual ICollection<DistributorProductDamage> DistributorProductDamages { get; set; }
        public virtual ICollection<DistributorOrderChange> DistributorOrderChanges { get; set; }

        public virtual ICollection<OutletOrderList> OutletOrderLists { get; set; }
        public virtual ICollection<OutletProductDamage> OutletProductDamages { get; set; }
        public virtual ICollection<OutletOrderChange> OutletOrderChanges { get; set; }


    }
}