using ORDER_MANAGEMENT.Data;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin,Areas")]
    public class AreasController : Controller
    {
        private readonly IUnitOfWork _db;
        public AreasController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        // GET: Areas
        public ActionResult Index()
        {
            ViewBag.RegionID = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName");
            return View();
        }

        // GET: Areas table
        public ActionResult GetAreaTable(string ids)
        {
            var serializer = new JavaScriptSerializer();
            var regionIds = serializer.Deserialize<List<int>>(ids);

            var list = _db.Areas.GetAreaByRegion(regionIds);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: Areas/Create
        public ActionResult Create()
        {
            ViewBag.RegionID = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName");
            return View("_Create");
        }

        // POST: Areas/Create
        [HttpPost]
        public ActionResult Create(Area model)
        {
            ViewBag.RegionID = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName", model.RegionID);

            var exist = _db.Areas.Any(n => n.AreaName == model.AreaName);
            if (exist) ModelState.AddModelError("AreaName", "Area Name already exist!");

            if (!ModelState.IsValid) return View("_Create", model);

            _db.Areas.Add(model);

            var task = _db.SaveChanges();
            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to insert record!");
            return View("_Create", model);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = _db.Areas.Find(id.GetValueOrDefault());
            ViewBag.RegionID = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName", model.RegionID);
            if (Request.IsAjaxRequest()) return PartialView("_Edit", model);

            return View(model);
        }

        // POST: Areas/Edit/5
        [HttpPost]
        public ActionResult Edit(Area model)
        {
            ViewBag.RegionID = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName", model.RegionID);

            var exist = _db.Areas.Any(n => (n.AreaName == model.AreaName) && (n.AreaID != model.AreaID));
            if (exist) ModelState.AddModelError("AreaName", "Area Name must be unique!");

            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);

            _db.Areas.Update(model);
            var task = _db.SaveChanges();

            if (task != 0)
                return Request.IsAjaxRequest() ? (ActionResult)Content("success") : RedirectToAction($"Index");

            ModelState.AddModelError("", "Unable to update");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);
        }


        // POST: Areas/Delete/5
        [HttpPost]
        public int Delete(int id)
        {
            var area = _db.Areas.Find(id);
            _db.Areas.Remove(area);
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
