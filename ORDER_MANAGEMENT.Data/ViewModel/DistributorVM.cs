using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORDER_MANAGEMENT.Data
{
    public class DistributorCreateVM
    {
        public int TerritoryID { get; set; }
        public int ReportTo_RegistrationID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public byte[] Photo { get; set; }
    }

    public class DistributorListBasicVM
    {
        public int DistributorID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsApproved { get; set; }
    }
    public class DistributorListVM : DistributorListBasicVM
    {
        public int DueRangeLimit { get; set; }
        public double TotalDue { get; set; }
        public byte[] Photo { get; set; }
    }

    public class DistributorDueList : DistributorListVM
    {
        public double TotalPaid { get; set; }
        public double TotalAmount { get; set; }

    }
    public class DistributorListWithUserVM : DistributorListVM
    {
        public Registration ReportTo_User { get; set; }
        public string TerritoryName { get; set; }
        public int RegionID { get; set; }
        public string DepotName { get; set; }
        public int? DepotId { get; set; }

    }

    public class DistributorOrderPlace
    {
        public DistributorOrderPlace()
        {
            this.Carts = new HashSet<DistributorCart>();
        }
        public int DistributorID { get; set; }
        public int OrderBy_RegistrationID { get; set; }
        public double OrderTotalPrice { get; set; }
        public double OrderDiscount { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public ICollection<DistributorCart> Carts { get; set; }
    }
    public class DistributorCart
    {
        public int DistributorOrderListID { get; set; }
        public int ProductID { get; set; }
        public int OrderQuantity { get; set; }
        public double UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }
    public class DistributorOrdered
    {
        public int DistributorOrderID { get; set; }
        public int DistributorID { get; set; }
        public int DistributorOrder_SN { get; set; }
        public string TerritoryName { get; set; }
        public string OrderBy_Name { get; set; }
        public string DistributorName { get; set; }
        public string DistributorAddress { get; set; }
        public double OrderTotalPrice { get; set; }
        public double OrderDiscount { get; set; }
        public double OrderNetPrice { get; set; }
        public bool Is_Approved { get; set; }
        public DateTime InsertDate { get; set; }

    }
    public class DistributorProductApproved
    {
        public int ProductID { get; set; }
        public int DistributorOrderListID { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int OrderQuantity { get; set; }
        public int ChangedQuantity { get; set; }
        public double UnitPrice { get; set; }
        public string Reason { get; set; }
    }
    public class DistributorOrderApproved
    {
        public DistributorOrderApproved()
        {
            this.OrderProducts = new List<DistributorProductApproved>();
        }
        public DistributorOrdered OrderInfo { get; set; }
        public List<DistributorProductApproved> OrderProducts { get; set; }
    }

    public class DistributorOrderDetails
    {

        public DistributorOrderDetails()
        {
            this.OrderProducts = new List<DistributorCart>();
        }
        public DistributorOrdered OrderInfo { get; set; }
        public List<DistributorCart> OrderProducts { get; set; }
    }

    public class DistributorOrderReturnItem
    {
        public int DistributorProductReturnID { get; set; }
        public int DistributorOrderListID { get; set; }
        public int ProductID { get; set; }
        public int OrderQuantity { get; set; }
        public int ReturnQuantity { get; set; }
        public double UnitPrice { get; set; }
        public double ReturnAmount { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
    }
    public class DistributorOrderReturnDetails
    {
        public DistributorOrderReturnDetails()
        {
            this.OrderReturnItems = new List<DistributorOrderReturnItem>();
        }
        public DistributorOrdered OrderInfo { get; set; }
        public List<DistributorOrderReturnItem> OrderReturnItems { get; set; }
    }

    public class Distributor_SR_ByTerritory
    {
        public Distributor_SR_ByTerritory()
        {
            this.Distributors = new HashSet<DDL>();
            this.SRs = new HashSet<DDL>();

        }
        public ICollection<DDL> Distributors { get; set; }
        public ICollection<DDL> SRs { get; set; }
    }

    public class DistirubtorRegisterVM
    {
        public int DistributorID { get; set; }
        [Required]
        [Display(Name = "Due Range")]
        public int DueRange { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


    }
    public enum DistributorTypes
    {
        All,
        Aapproved,
        unapproved
    }
}
