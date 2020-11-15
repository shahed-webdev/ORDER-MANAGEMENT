using Newtonsoft.Json;
using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    public class OutletController : Controller
    {
        private readonly IUnitOfWork _db;
        public OutletController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [Authorize(Roles = "Admin,Outlets")]
        // GET: Outlet
        public ActionResult Index()
        {
            var model = _db.Outlets.OutletList();
            return View(model);
        }

        // GET: Distributor/Approved
        public int Approved(int OutletID, int DueRange = 0)
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.Outlets.Approved(OutletID, RegID, DueRange);

            return _db.SaveChanges();
        }

        [Authorize(Roles = "Admin,Outlet_Location Map")]
        //GET: Location Map
        public ActionResult LocationMap()
        {
            return View();
        }

        public string GetOutletList()
        {
            var list = _db.Outlets.OutletList();
            return JsonConvert.SerializeObject(list);
        }

        //GET: OrderList
        [Authorize(Roles = "Admin,Outlet_OrderList")]
        public ActionResult OrderList()
        {
            return View();
        }

        //Transfer Record data-table
        public JsonResult GetOutletOrders(DataRequest request)
        {
            var result = _db.DepotProductTransfers.ListDataTable(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
