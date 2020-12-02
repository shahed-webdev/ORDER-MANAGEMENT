using ORDER_MANAGEMENT.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace ORDER_MANAGEMENT.API.Controllers
{
    public class OutletController : ApiController
    {
        private readonly IUnitOfWork _db;
        public OutletController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        // POST: api/Outlet
        [HttpPost]
        [Route("api/Outlet")]
        public IHttpActionResult Post([FromBody] OutletCreateVM value)
        {
            if (value == null) NotFound();

            var isExist = _db.Outlets.IsExist(value.Phone);
            if (isExist)
                return Content(HttpStatusCode.BadRequest, "Phone Number Already Exist!");


            value.CreateBy_RegistrationID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var user = _db.Users.Find(value.CreateBy_RegistrationID);
            value.DistributorID = user.DistributorID.GetValueOrDefault();

            if (user.DistributorID == null)
                return Content(HttpStatusCode.NotFound, "Distributor Not Assigned");

            //Update because of Distributor multiple Territory option add
            //value.TerritoryID = db.Distributors.Find(value.DistributorID).TerritoryID;

            _db.Outlets.CreateOutlet(value);
            _db.SaveChanges();

            return Ok();
        }


        [HttpGet]
        // GET: api/OutletList
        [Route("api/getTerritoryDDL")]
        public List<DDL> getTerritoryDdl()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            var distributorId = _db.Users.Find(id).DistributorID;
            if (distributorId == null) return new List<DDL>();

            var territoryDdl = _db.Territorys.GetDistributorTerritory(distributorId.GetValueOrDefault());
            return territoryDdl;
        }

        [HttpGet]
        // GET: api/OutletList
        [Route("api/getOutletList")]
        public List<OutletListVM> getOutletList()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Outlets = _db.Outlets.OutletListByUser(id);
            return Outlets;
        }

        [HttpGet]
        // GET: api/getOutletRoutePlan
        [Route("api/getOutletRoutePlan")]
        public List<OutletListVM> getOutletRoutePlan()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Outlets = _db.UserRoutes.getOutletByUserRoute(id);
            return Outlets;
        }

        [HttpPost]
        [Route("api/OutletCheckIn")]
        public IHttpActionResult OutletCheckIn([FromBody] UserTrackingByOutlet value)
        {
            if (value == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            value.RegistrationID = id;
            value.TrackingDate = DateTime.Today;
            value.TrackingDateTime = DateTime.Now;

            _db.UserTrackingByOutlets.checkIn(value);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/OutletOrder")]
        public IHttpActionResult OutletOrder([FromBody] OutletOrderPlace value)
        {
            if (value == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Receipt = _db.OutletOrders.OrderPlaced(value, id);
            _db.SaveChanges();

            return Ok(Receipt);
        }

        [HttpGet]
        [Route("api/OutletDueLimit/{id}")]
        public IHttpActionResult OutletDueLimit(int id)
        {
            double? limit = _db.Outlets.GetDueRange(id);
            return Ok(limit);
        }

        [HttpGet]
        [Route("api/GetOutletOrderHistory/{id}")]
        public IHttpActionResult GetOutletOrderHistory(int id)
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = _db.OutletOrders.OrderHistory(RegID, id);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/OutletOrderDetails/{id}")]
        public IHttpActionResult OutletOrderDetails(int id)
        {
            var model = _db.OutletOrders.OrderDetails(id);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/GetOutletUndeliveredOrderList")]
        public IHttpActionResult UndeliveredOrderList()
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = _db.OutletOrders.UndeliveredOrderList_ByUser(RegID);
            return Ok(model);
        }

        [HttpGet]
        [Route("api/GetOutletOrderedList")]
        public IHttpActionResult GetOutletOrderedList()
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var model = _db.OutletOrders.OrderedHistory(RegID);
            return Ok(model);
        }

        [HttpPost]
        [Route("api/OutletOrderReturn")]
        public IHttpActionResult OutletOrderReturn([FromBody] List<OutletProductReturn> values)
        {
            if (values == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.OutletProductReturns.ApprovedReturn(values, id);
            _db.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("api/OutletDeliveryDetails/{id}")]
        public IHttpActionResult OutletDeliveryDetails(int id)
        {
            var model = _db.OutletOrders.OrderDeliveredDetails(id);
            return Ok(model);
        }

        [HttpPost]
        [Route("api/OutletDeliveredProduct")]
        public IHttpActionResult OutletDeliverdProduct(OutletOrderDelivered model)
        {

            var RegistrationID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var r = _db.OutletOrders.OrderDelivered(model, RegistrationID);
            if (r == 0) _db.SaveChanges();
            return Ok(r);
        }
        [HttpGet]
        // GET: api/Outlet/getOutletList
        [Route("api/getOutletDueList")]
        public List<OutletDueList> getOutletDueList()
        {
            //var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var Outlets = _db.OutletPaymentRecords.OutletDueList();
            return Outlets;
        }

        [HttpPost]
        [Route("api/OutletDuePay")]
        public IHttpActionResult OutletDuePay([FromBody] OutletPaymentRecord value)
        {
            if (value == null) NotFound();

            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            value.PaymentDate = DateTime.Now;
            _db.OutletPaymentRecords.PayDue(value);
            _db.SaveChanges();

            return Ok();
        }
    }
}
