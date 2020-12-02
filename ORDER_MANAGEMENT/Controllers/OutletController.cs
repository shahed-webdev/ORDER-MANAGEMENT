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


        // GET: Outlet
        [Authorize(Roles = "Admin,Outlets")]
        public ActionResult Index()
        {
            var model = _db.Outlets.OutletList();
            return View(model);
        }

        // GET: delete outlet
        [HttpPost]
        public ActionResult DeleteOutlet(int outletId)
        {
            var response = _db.Outlets.DeleteOutlet(outletId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        // GET: Approved
        public int Approved(int outletId, int dueRange = 0)
        {
            var regId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.Outlets.Approved(outletId, regId, dueRange);

            return _db.SaveChanges();
        }

        // GET: Due Range Update
        public int DueRangeUpdate(int outletId, int dueRange = 0)
        {
            _db.Outlets.DueRangeChange(outletId, dueRange);
            return _db.SaveChanges();
        }

        //GET: Update outlet
        public ActionResult Update(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            var response = _db.Outlets.GetDetails(id.GetValueOrDefault());
            
            if(!response.IsSuccess)
                return RedirectToAction("Index");

            return View(response.Data);
        }

        //POST: Update outlet
        //public ActionResult Update(OutletDetailsUpdateModel model)
        //{
        //    var response = _db.Outlets
        //    return View(model1);
        //}

        //GET: Location Map
        [Authorize(Roles = "Admin,Outlet_Location Map")]
        public ActionResult LocationMap()
        {
            return View();
        }

        public string GetOutletList()
        {
            var list = _db.Outlets.OutletList();
            return JsonConvert.SerializeObject(list);
        }

        //GET: Order List
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
