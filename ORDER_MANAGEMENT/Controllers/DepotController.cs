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
    }
}