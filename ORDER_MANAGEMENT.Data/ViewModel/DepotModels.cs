using System;

namespace ORDER_MANAGEMENT.Data
{
    public class DepotViewModel
    {
        public int DepotId { get; set; }
        public int RegionID { get; set; }
        public string DepotName { get; set; }
        public string RegionName { get; set; }
        public string Incharge { get; set; }
    }

    public class DepotStockAdd
    {
        public int DepotId { get; set; }
        public int ProductID { get; set; }
        public int RegistrationID { get; set; }
        public int Quantity { get; set; }
    }

    public class DepotProductTransferViewModel
    {
        public int DepotProductTransferId { get; set; }
        public int DepotId { get; set; }
        public string DepotName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string TransferBy { get; set; }
        public int Quantity { get; set; }
        public DateTime InsertDate { get; set; }
    }

    public class DepotReturnAddModel
    {
        public int DepotId { get; set; }
        public int ProductID { get; set; }
        public int RegistrationID { get; set; }
        public int Quantity { get; set; }
    }

    public class DepotProductReturnViewModel
    {
        public int DepotProductReturnId { get; set; }
        public int DepotId { get; set; }
        public string DepotName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ReturnBy { get; set; }
        public int Quantity { get; set; }
        public DateTime InsertDate { get; set; }
    }
}