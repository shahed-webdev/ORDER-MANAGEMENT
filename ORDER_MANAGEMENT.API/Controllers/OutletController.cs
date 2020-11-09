using ORDER_MANAGEMENT.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ORDER_MANAGEMENT.API.Controllers
{
    public class OutletController : ApiController
    {
        private readonly IUnitOfWork db;
        public OutletController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        // POST: api/Outlet
        [HttpPost]
        [Route("api/Outlet")]
        public IHttpActionResult Post([FromBody]OutletCreateVM value)
        {
            if (value == null) NotFound();

            value.CreateBy_RegistrationID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var user = db.Users.Find(value.CreateBy_RegistrationID);
            value.DistributorID = user.DistributorID.GetValueOrDefault();

            if (user.DistributorID == null)
            {
                return Content(HttpStatusCode.NotFound, "Distributor Not Assigned");
            }


            value.TerritoryID = db.Distributors.Find(value.DistributorID).TerritoryID;

            db.Outlets.CreateOutlet(value);
            db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        // GET: api/OutletList
        [Route("api/getOutletList")]
        public List<OutletListVM> getOutletList()
        {
            // var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Outlets = db.Outlets.OutletList();
            return Outlets;
        }

        [HttpGet]
        // GET: api/getOutletRoutePlan
        [Route("api/getOutletRoutePlan")]
        public List<OutletListVM> getOutletRoutePlan()
        {
            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Outlets = db.UserRoutes.getOutletByUserRoute(id);
            return Outlets;
        }

        [HttpPost]
        [Route("api/OutletCheckIn")]
        public IHttpActionResult OutletCheckIn([FromBody]UserTrackingByOutlet value)
        {
            if (value == null) NotFound();

            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            value.RegistrationID = id;
            value.TrackingDate = DateTime.Today;
            value.TrackingDateTime = DateTime.Now;

            db.UserTrackingByOutlets.checkIn(value);
            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/OutletOrder")]
        public IHttpActionResult OutletOrder([FromBody]OutletOrderPlace value)
        {
            if (value == null) NotFound();

            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Receipt = db.OutletOrders.OrderPlaced(value, id);
            db.SaveChanges();

            return Ok(Receipt);
        }

        [HttpGet]
        [Route("api/OutletDueLimit/{id}")]
        public IHttpActionResult OutletDueLimit(int id)
        {

            double? limit = db.Outlets.GetDueRange(id);
            if (limit == null) return NotFound();

            return Ok(limit);
        }

        [HttpGet]
        [Route("api/GetOutletOrderHistory/{id}")]
        public IHttpActionResult GetOutletOrderHistory(int id)
        {
            var RegID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = db.OutletOrders.OrderHistory(RegID, id);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/OutletOrderDetails/{id}")]
        public IHttpActionResult OutletOrderDetails(int id)
        {
            var model = db.OutletOrders.OrderDetails(id);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/GetOutletUndeliveredOrderList")]
        public IHttpActionResult UndeliveredOrderList()
        {
            var RegID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = db.OutletOrders.UndeliveredOrderList_ByUser(RegID);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/GetOutletOrderedList")]
        public IHttpActionResult GetOutletOrderedList()
        {
            var RegID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = db.OutletOrders.OrderedHistory(RegID);
            return Ok(model);
        }

        [HttpPost]
        [Route("api/OutletOrderReturn")]
        public IHttpActionResult OutletOrderReturn([FromBody]List<OutletProductReturn> values)
        {
            if (values == null) NotFound();

            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            db.OutletProductReturns.ApprovedReturn(values, id);
            db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("api/OutletDeliveryDetails/{id}")]
        public IHttpActionResult OutletDeliveryDetails(int id)
        {
            var model = db.OutletOrders.OrderDeliveredDetails(id);
            return Ok(model);
        }

        [HttpPost]
        [Route("api/OutletDeliveredProduct")]
        public IHttpActionResult OutletDeliverdProduct(OutletOrderDelivered model)
        {

            var RegistrationID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var r = db.OutletOrders.OrderDelivered(model, RegistrationID);
            if (r == 0) db.SaveChanges();
            return Ok(r);
        }
        [HttpGet]
        // GET: api/Outlet/getOutletList
        [Route("api/getOutletDueList")]
        public List<OutletDueList> getOutletDueList()
        {
            //var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Outlets = db.OutletPaymentRecords.OutletDueList();
            return Outlets;
        }

        [HttpPost]
        [Route("api/OutletDuePay")]
        public IHttpActionResult OutletDuePay([FromBody]OutletPaymentRecord value)
        {
            if (value == null) NotFound();

            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            value.PaymentDate = DateTime.Now;
            db.OutletPaymentRecords.PayDue(value);
            db.SaveChanges();

            return Ok();
        }
    }
}
