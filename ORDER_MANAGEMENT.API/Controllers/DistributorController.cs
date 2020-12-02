using ORDER_MANAGEMENT.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ORDER_MANAGEMENT.API.Controllers
{
    [Authorize]
    public class DistributorController : ApiController
    {
        private readonly IUnitOfWork _db;
        public DistributorController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [HttpGet]
        // GET: api/Distributor/getDistributorList
        [Route("api/getDistributorList")]
        public List<DistributorListVM> getDistributorList()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var distributors = _db.Distributors.DistributorListByUser(id);
            return distributors;
        }

        [HttpGet]
        // GET: api/Distributor/getDistributorList
        [Route("api/getDistributorRoutePlan")]
        public List<DistributorListVM> getDistributorRoutePlan()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var distributors = _db.UserRoutes.getDistributorByUserRoute(id);
            return distributors;
        }

        [HttpGet]
        [Route("api/getTerriroryDDL")]
        public List<DDL> getTerriroryDDL()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var TerriroryDDL = _db.Territorys.GetUserTerritory(id);
            return TerriroryDDL;
        }
       
        
        [HttpPost]
        // POST: api/Distributor
        [Route("api/Distributor")]
        public IHttpActionResult Post([FromBody] DistributorCreateVM value)
        {
            if (value == null) NotFound();
            
            var isExist = _db.Distributors.IsExist(value.Mobile);
            if (isExist)
                return Content(HttpStatusCode.BadRequest, "Phone Number Already Exist!");

            value.ReportTo_RegistrationID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.Distributors.CreateDistributor(value);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/DistributorOrder")]
        public IHttpActionResult DistributorOrder([FromBody] DistributorOrderPlace value)
        {
            if (value == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Receipt = _db.DistributorOrders.OrderPlaced(value, id);
            _db.SaveChanges();

            return Ok(Receipt);
        }

        [HttpPost]
        [Route("api/DistributorCheckIn")]
        public IHttpActionResult DistributorCheckIn([FromBody] UserTrackingByDistributor value)
        {
            if (value == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            value.RegistrationID = id;
            value.TrackingDate = DateTime.Today;
            value.TrackingDateTime = DateTime.Now;

            _db.UserTrackingByDistributors.checkIn(value);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("api/GetDistributorUnapprovedOrderList")]
        public IHttpActionResult GetDistributorOrderList()
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = _db.DistributorOrders.UnapprovedOrderList_ByUser(RegID);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/GetDistributorOrderHistory/{id}")]
        public IHttpActionResult GetDistributorOrderHistory(int id)
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = _db.DistributorOrders.OrderHistory(RegID, id);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/GetDistributorOrderedList")]
        public IHttpActionResult GetDistributorOrderedList()
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = _db.DistributorProductReturns.OrderedHistory(RegID);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/DistributorOrderDetails/{id}")]
        public IHttpActionResult DistributorOrderDetails(int id)
        {
            var model = _db.DistributorOrders.OrderDetails(id);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/DistributorDueLimit/{id}")]
        public IHttpActionResult DistributorDueLimit(int id)
        {

            double? limit = _db.Distributors.GetDueRange(id);
            if (limit == null) return NotFound();

            return Ok(limit);
        }

        [HttpPost]
        [Route("api/DistributorOrderReturn")]
        public IHttpActionResult DistributorOrderReturn([FromBody] List<DistributorProductReturn> values)
        {
            if (values == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            foreach (var value in values)
            {
                value.ReturnBy_RegistrationID = id;
                value.ReturnDate = DateTime.Now;
            }
            _db.DistributorProductReturns.AddRange(values);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        // GET: api/Distributor/getDistributorList
        [Route("api/getDistributorDueList")]
        public List<DistributorDueList> getDistributorDueList()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var distributors = _db.DistributorPaymentRecords.DistributorDueListByUser(id);
            return distributors;
        }

        [HttpPost]
        [Route("api/DistributorDuePay")]
        public IHttpActionResult DistributorDuePay([FromBody] DistributorPaymentRecord value)
        {
            if (value == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            value.PaymentDate = DateTime.Now;

            _db.DistributorPaymentRecords.PayDue(value);
            _db.SaveChanges();

            return Ok();
        }
    }
}
