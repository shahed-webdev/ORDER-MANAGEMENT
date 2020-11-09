using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin,EquipmentType")]
    public class EquipmentTypeController : Controller
    {
        private readonly IUnitOfWork _db;
        public EquipmentTypeController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        // GET: EquipmentType
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll(DataRequest request)
        {
            var data = _db.EquipmentTypes.DT(request);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        // GET: EquipmentType/Create
        public ActionResult Create()
        {
            var model = new EquipmentType();
            return View("_Create", model);
        }

        // POST: EquipmentType/Create
        [HttpPost]
        public ActionResult Create(EquipmentType model)
        {
            var exist = _db.EquipmentTypes.Any(n => n.EquipmentTypeName == model.EquipmentTypeName);
            if (exist) ModelState.AddModelError("EquipmentTypeName", "Type Name already exist!");

            if (!ModelState.IsValid) return View("_Create", model);

            _db.EquipmentTypes.Add(model);
            var task = _db.SaveChanges();
            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to insert record!");
            return View("_Create", model);
        }

        // GET: EquipmentType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = _db.EquipmentTypes.Find(id.GetValueOrDefault());

            if (Request.IsAjaxRequest()) return PartialView("_Edit", model);

            return View(model);
        }

        // POST: EquipmentType/Edit/5
        [HttpPost]
        public ActionResult Edit(EquipmentType model)
        {
            var exist = _db.EquipmentTypes.Any(n => n.EquipmentTypeName == model.EquipmentTypeName && n.EquipmentTypeID != model.EquipmentTypeID);
            if (exist) ModelState.AddModelError("EquipmentTypeName", "Type Name already exist!");

            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);

            _db.EquipmentTypes.Update(model);
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

        // POST: EquipmentType/Delete/5
        public int Delete(int id)
        {
            var model = _db.EquipmentTypes.Find(id);
            _db.EquipmentTypes.Remove(model);
            return _db.SaveChanges();
        }
    }
}
