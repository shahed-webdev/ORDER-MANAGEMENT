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

        // GET: Approved
        public int Approved(int outletId, int dueRange = 0)
        {
            var regId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.Outlets.Approved(outletId, regId, dueRange);

            return _db.SaveChanges();
        }

        // GET: Approved
        public int DueRangeUpdate(int outletId, int dueRange = 0)
        {
            _db.Outlets.DueRangeChange(outletId, dueRange);
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
            var result = _db.OutletOrders.OrderedDataTable(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
