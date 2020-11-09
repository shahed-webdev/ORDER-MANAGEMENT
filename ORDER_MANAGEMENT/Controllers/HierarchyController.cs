using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class HierarchyController : Controller
    {
        private readonly IUnitOfWork _db;

        public HierarchyController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [Authorize(Roles = "Admin,Hierarchy")]
        // GET: Hierarchy
        public ActionResult Index()
        {
            return View();
        }

        // GET: Hierarchy data-table
        public async Task<ActionResult> GetAll()
        {
            var data = await _db.Hierarchys.GetAllAsync();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Hierarchy")]
        // GET: Hierarchy/Create
        public ActionResult Create()
        {
            var model = new Organization_hierarchy();
            return View("_Create", model);
        }

        // POST: Hierarchy/Create
        [HttpPost]
        public async Task<ActionResult> Create(Organization_hierarchy oh)
        {
            var exist = _db.Hierarchys.Any(n => n.HierarchyName == oh.HierarchyName || n.Rank == oh.Rank);

            if (exist) ModelState.AddModelError("HierarchyName", "Hierarchy Name already exist!");

            if (!ModelState.IsValid) return View("_Create", oh);


            oh.CreateRegistrationID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.Hierarchys.Add(oh);

            var task = await _db.SaveChangesAsync();
            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to insert record!");
            return View("_Create", oh);

        }

        [Authorize(Roles = "Admin,Hierarchy")]
        // GET: Hierarchy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var hierarchy = _db.Hierarchys.Find(id.GetValueOrDefault());

            if (hierarchy == null) return HttpNotFound();
            if (Request.IsAjaxRequest()) return PartialView("_Edit", hierarchy);

            return View(hierarchy);
        }

        // POST: Hierarchy/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Organization_hierarchy oh)
        {
            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", oh);

            var exist = _db.Hierarchys.Any(n => (n.HierarchyName == oh.HierarchyName || n.Rank == oh.Rank) && n.HierarchyID != oh.HierarchyID);

            if (!exist)
            {
                _db.Hierarchys.Update(oh);
                var task = await _db.SaveChangesAsync();

                if (task == 0)
                {
                    ModelState.AddModelError("", "Unable to update");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", oh);
                }
            }
            else
            {
                ModelState.AddModelError("", "Hierarchy Name and Rank must be unique!");
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }

            return RedirectToAction("Index");
        }


        // POST: Hierarchy/Delete/5
        public int Delete(int id)
        {
            var oh = _db.Hierarchys.Find(id);
            _db.Hierarchys.Remove(oh);

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
