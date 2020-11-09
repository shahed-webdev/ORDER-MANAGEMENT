using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin,ProductMainCategories")]
    public class ProductMainCategoryController : Controller
    {
        private readonly IUnitOfWork _db;
        public ProductMainCategoryController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        // GET: ProductMainCategory
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll(DataRequest request)
        {
            var result = _db.ProductMainCategorys.DT(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: ProductMainCategory/Create
        public ActionResult Create()
        {
            var model = new ProductMainCategory();
            return View("_Create", model);
        }

        // POST: ProductMainCategory/Create
        [HttpPost]
        public ActionResult Create(ProductMainCategory model)
        {
            var exist = _db.ProductMainCategorys.Any(n => n.ProductMainCategoryName == model.ProductMainCategoryName);
            if (exist) ModelState.AddModelError("ProductMainCategoryName", "Category Name already exist!");

            if (!ModelState.IsValid) return View("_Create", model);

            _db.ProductMainCategorys.Add(model);
            var task = _db.SaveChanges();
            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to insert record!");
            return View("_Create", model);
        }

        // GET: ProductMainCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = _db.ProductMainCategorys.Find(id.GetValueOrDefault());

            if (model == null) return HttpNotFound();
            if (Request.IsAjaxRequest()) return PartialView("_Edit", model);

            return View(model);
        }

        // POST: ProductMainCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductMainCategory model)
        {
            var exist = _db.ProductMainCategorys.Any(n => n.ProductMainCategoryName == model.ProductMainCategoryName && n.ProductMainCategoryID != model.ProductMainCategoryID);
            if (exist) ModelState.AddModelError("ProductMainCategoryName", "Category Name already exist!");

            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);

            _db.ProductMainCategorys.Update(model);
            var task = _db.SaveChanges();

            if (task == 0)
            {
                ModelState.AddModelError("", "Unable to update");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);
            }

            if (Request.IsAjaxRequest())
            {
                return Content("success");
            }

            return RedirectToAction("Index");
        }

        // GET: ProductMainCategory/Delete/5
        public int Delete(int id)
        {
            var model = _db.ProductMainCategorys.Find(id);
            _db.ProductMainCategorys.Remove(model);
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
