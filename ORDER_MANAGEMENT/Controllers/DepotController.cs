using System.Linq;
using System.Web.Mvc;
using ORDER_MANAGEMENT.Data;

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
            //var model = _db.Depots.GetAll();
            return View();
        }

        // GET: Create
        public ActionResult Create()
        {
            using (var db = new DataContext())
            {
                ViewBag.Hierarchy = db.Organization_hierarchy.Where(h => h.Rank > 1).Select(n => new SelectListItem { Value = n.Rank.ToString(), Text = n.HierarchyName }).ToList();
            }
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