using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin,EquipmentAdd")]
    public class EquipmentAddController : Controller
    {
        private readonly IUnitOfWork _db;
        public EquipmentAddController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        // GET: EquipmentAdd
        public ActionResult Index()
        {
            ViewBag.EquipmentType = new SelectList(_db.EquipmentTypes.GetDdl(), "value", "label");
            return View();
        }

        public JsonResult GetAll(DataRequest request, int[] filter)
        {
            var data = _db.Equipments.DT_TypeWise(request, filter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: EquipmentAdd/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentTypeID = new SelectList(_db.EquipmentTypes.GetDdl(), "value", "label");
            var model = new Equipment();
            return View("_Create", model);
        }

        // POST: EquipmentAdd/Create
        [HttpPost]
        public ActionResult Create(Equipment model)
        {
            ViewBag.EquipmentTypeID = new SelectList(_db.EquipmentTypes.GetDdl(), "value", "label");

            var exist = _db.Equipments.Any(n => n.Code == model.Code);
            if (exist) ModelState.AddModelError("Code", "Code already exist!");

            if (!ModelState.IsValid) return View("_Create", model);

            _db.Equipments.Add(model);
            var task = _db.SaveChanges();
            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to insert record!");
            return View("_Create", model);
        }

        // GET: EquipmentAdd/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = _db.Equipments.Find(id.GetValueOrDefault());
            ViewBag.EquipmentTypeID = new SelectList(_db.EquipmentTypes.GetDdl(), "value", "label", model.EquipmentTypeID);

            if (Request.IsAjaxRequest()) return PartialView("_Edit", model);

            return View(model);
        }

        // POST: EquipmentAdd/Edit
        [HttpPost]
        public ActionResult Edit(Equipment model)
        {
            ViewBag.EquipmentTypeID = new SelectList(_db.EquipmentTypes.GetDdl(), "value", "label", model.EquipmentTypeID);

            var exist = _db.Equipments.Any(n => n.Code == model.Code && n.EquipmentID != model.EquipmentID);
            if (exist) ModelState.AddModelError("Code", "Code already exist!");

            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);

            _db.Equipments.Update(model);
            var task = _db.SaveChanges();

            if (task == 0)
            {
                ModelState.AddModelError("", "Unable to update");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);
            }

            if (Request.IsAjaxRequest()) return Content("success");

            return RedirectToAction("Index");
        }

        //GET: Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.id = id;

            var model = _db.Equipments.Details(id.GetValueOrDefault());
            return View(model);
        }

        //POST: Details
        [HttpPost]
        public ActionResult Details(EquipmentDistributionVM model)
        {
            if (!ModelState.IsValid) return View(model);
            model.AssignByRegistrationID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);


            _db.EquipmentDistributions.DistributionRemove(model.EquipmentID, model.InstalledDate.GetValueOrDefault());
            _db.EquipmentDistributions.Distribution(model);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult FindOutlet(string prefix, int currentOutletId)
        {
            var data = _db.Outlets.Search(prefix, currentOutletId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // POST: EquipmentAdd/Delete/5
        [HttpPost]
        public int Delete(int id)
        {
            var model = _db.Equipments.Find(id);
            _db.Equipments.Remove(model);
            return _db.SaveChanges();
        }
    }
}
