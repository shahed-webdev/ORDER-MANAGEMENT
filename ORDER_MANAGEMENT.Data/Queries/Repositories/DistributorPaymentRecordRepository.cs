using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace ORDER_MANAGEMENT.Data
{
    public class DistributorPaymentRecordRepository : Repository<DistributorPaymentRecord>, IDistributorPaymentRecordRepository
    {
        public DistributorPaymentRecordRepository(DataContext context) : base(context)
        {

        }

        public List<DistributorDueList> DistributorDueListByUser(int id)
        {
            var Distributors = (from d in Context.Distributors
                                where d.ReportTo_RegistrationID == id && d.Total_DueAmount > 0
                                orderby d.DistributorID descending
                                select new DistributorDueList
                                {
                                    Address = d.Address,
                                    DueRangeLimit = d.DueRangeLimit,
                                    InsertDate = d.InsertDate,
                                    Lat = d.Lat,
                                    Lon = d.Lon,
                                    Mobile = d.Mobile,
                                    Name = d.Name,
                                    TotalDue = d.Total_DueAmount,
                                    TotalAmount = d.Total_BuyingAmount - d.Total_ReturnAmount,
                                    TotalPaid = d.Total_PaidAmount,
                                    Photo = d.Photo,
                                    DistributorID = d.DistributorID,
                                    IsApproved = d.IsApproved
                                }).ToList();
            return Distributors;
        }

        public void PayDue(DistributorPaymentRecord model)
        {
            Add(model);

            //Distributor update
            var Distributor = Context.Distributors.Find(model.DistributorID);
            Distributor.Total_PaidAmount += model.Amount;
            Context.Entry(Distributor).State = EntityState.Modified;

        }
    }
}
