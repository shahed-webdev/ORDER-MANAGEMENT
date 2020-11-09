using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = ("Admin,Regions"))]
    public class RegionsController : Controller
    {
        private readonly IUnitOfWork _db;
        public RegionsController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        // GET: Regions
        public ActionResult Index()
        {
            return View(_db.Regions.GetAll());
        }

        // GET: Regions/Create
        public ActionResult Create()
        {
            return View("_Create");
        }

        // POST: Regions/Create
        [HttpPost]
        public async Task<ActionResult> Create(Region region)
        {
            if (!ModelState.IsValid) return View("_Create", region);

            _db.Regions.Add(region);

            var task = await _db.SaveChangesAsync();

            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to insert record!");
            return View("_Create", region);

        }

        // GET: Regions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = _db.Regions.Find(id.GetValueOrDefault());

            if (model == null) return HttpNotFound();
            if (Request.IsAjaxRequest()) return PartialView($"_Edit", model);

            return View(model);
        }


        // POST: Regions/Edit/5
        [HttpPost]
        public ActionResult Edit(Region model)
        {
            var exist = _db.Regions.Any(n => (n.RegionName == model.RegionName) && (n.RegionID != model.RegionID));
            if (exist) ModelState.AddModelError("RegionName", "Region Name must be unique!");

            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);

            _db.Regions.Update(model);
            var task = _db.SaveChanges();

            if (task != 0)
                return Request.IsAjaxRequest() ? (ActionResult)Content("success") : RedirectToAction($"Index");

            ModelState.AddModelError("", "Unable to update");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);
        }


        // POST: Regions/Delete/5
        public int Delete(int id)
        {
            var region = _db.Regions.Find(id);
            _db.Regions.Remove(region);
            return _db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
