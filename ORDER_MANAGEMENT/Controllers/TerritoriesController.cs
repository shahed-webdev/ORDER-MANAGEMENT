using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = ("Admin,Sub-admin"))]
    public class TerritoriesController : Controller
    {
        private readonly IUnitOfWork db;
        public TerritoriesController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        // GET: Territories
        public ActionResult Index()
        {
            ViewBag.RegionID = new SelectList(db.Regions.GetDllRegion(), "RegionID", "RegionName");
            return View();
        }

        // GET: Territories/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Areas.GetAllArea(), "AreaID", "AreaName");
            return View();
        }

        // POST: Territories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Territory territory)
        {
            if (ModelState.IsValid)
            {
                db.Territorys.Add(territory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Areas.GetAllArea(), "AreaID", "AreaName", territory.AreaID);
            return View(territory);
        }

        // GET: Territories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var territory = db.Territorys.Find(id.GetValueOrDefault());
            if (territory == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Areas.GetAllArea(), "AreaID", "AreaName", territory.AreaID);
            return View(territory);
        }

        // POST: Territories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TerritoryID,AreaID,TerritoryName")] Territory territory)
        {
            if (ModelState.IsValid)
            {
                db.Territorys.Update(territory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Areas.GetAllArea(), "AreaID", "AreaName", territory.AreaID);
            return View(territory);
        }


        // POST: Territories/Delete/5
        [HttpPost]
        public int Delete(int id)
        {
            var territory = db.Territorys.Find(id);
            db.Territorys.Remove(territory);
            return db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
