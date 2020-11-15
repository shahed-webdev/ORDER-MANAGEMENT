using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin, Depot")]
    public class DepotController : Controller
    {
        private readonly IUnitOfWork _db;

        public DepotController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: Depot 
        public ActionResult Depot()
        {
            ViewBag.RegionID = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName");
            return View();
        }

        // GET: Depot data-table
        public ActionResult GetDepots(DataRequest request)
        {
            var result = _db.Depots.ListDataTable(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       // Stocks
        public ActionResult Stocks(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Depot");

            ViewBag.DepotId = id;
            ViewBag.MainCategory = new SelectList(_db.ProductMainCategorys.GetDdlforSub(), "value", "label");
            return View();
        }

        //return
        public ActionResult StockReturn()
        {
            return View();
        }

        //damage
        public ActionResult StockDamage()
        {
            return View();
        }

        //stock data-table
        public JsonResult GetCategoryProduct(DataRequest request, int[] filter)
        {
            var result = _db.DepotStocks.ProductsDataTable(request, filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Depot dropdown by region
        public ActionResult DepotDropDownByRegion(int regionId)
        {
            var data = _db.Depots.Ddls(regionId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Create
        public ActionResult Create()
        {

            ViewBag.Hierarchy = new SelectList(_db.Hierarchys.GetDll_Hierarchy(), "Rank", "HierarchyName");

            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(Depot model)
        {
            ViewBag.Hierarchy = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName", model.RegionID);

            var exist = _db.Depots.Any(n => n.DepotName == model.DepotName);
            if (exist) ModelState.AddModelError("DepotName", "Depot Name already exist!");

            if (!ModelState.IsValid) return View(model);

            _db.Depots.Add(model);

            var task = _db.SaveChanges();
            if (task == 0) return View(model);

            ModelState.AddModelError("", "Unable to insert record!");
            return RedirectToAction("Depot");
        }

        // GET: transfer record
        public ActionResult TransferRecord()
        {
            ViewBag.DepotId = new SelectList(_db.Depots.Ddls(), "value", "label");
            return View();
        }

        //Transfer Record data-table
        public JsonResult GetTransferRecord(DataRequest request)
        {
            var result = _db.DepotProductTransfers.ListDataTable(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        // GET: return record
        public ActionResult ReturnRecord()
        {
            ViewBag.DepotId = new SelectList(_db.Depots.Ddls(), "value", "label");
            return View();
        }

        //Return Record data-table
        public JsonResult GetReturnRecord(DataRequest request)
        {
            var result = _db.DepotProductTransfers.ListDataTable(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: damage record
        public ActionResult DamageRecord()
        {
            ViewBag.DepotId = new SelectList(_db.Depots.Ddls(), "value", "label");
            return View();
        }

        //Damage Record data-table
        public JsonResult GetDamageRecord(DataRequest request)
        {
            var result = _db.DepotProductTransfers.ListDataTable(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}