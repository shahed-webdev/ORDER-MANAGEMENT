using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin,Targets")]
    public class TargetsController : Controller
    {
        private readonly IUnitOfWork db;
        public TargetsController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        // GET: Targets
        public ActionResult Index()
        {
            return View();
        }

        // GET: Hierarchy datatable
        public async Task<ActionResult> GetAll()
        {
            var data = await db.Targets.GetAllAsync();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        // GET: Targets/Create
        public ActionResult Create()
        {
            return View("_Create");
        }

        // POST: Targets/Create
        [HttpPost]
        public async Task<ActionResult> Create(TargetVM target)
        {
            if (!ModelState.IsValid) return View("_Create", target);

            target.CreatedByRegistrationID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            db.Targets.CreateTarget(target);

            var task = await db.SaveChangesAsync();
            if (task == 0)
            {
                ModelState.AddModelError("", "Unable to insert record!");
                return View("_Create", target);
            }

            return Content("success");
        }

        // GET: Targets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var target = db.Targets.Find(id.GetValueOrDefault());
            if (target == null) return HttpNotFound();

            if (Request.IsAjaxRequest()) return PartialView("_Edit", target);

            return View(target);
        }

        // POST: Targets/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Target target)
        {
            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", target);

            db.Targets.Update(target);
            var task = await db.SaveChangesAsync();

            if (task == 0)
            {
                ModelState.AddModelError("", "Unable to update");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", target);
            }


            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }

            return RedirectToAction("Index");
        }


        // POST: Targets/Delete/5
        public int Delete(int id)
        {
            var target = db.Targets.Find(id);
            db.Targets.Remove(target);
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
