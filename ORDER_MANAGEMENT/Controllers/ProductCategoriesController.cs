using ORDER_MANAGEMENT.Data;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class ProductCategoriesController : Controller
    {
        private readonly IUnitOfWork _db;
        public ProductCategoriesController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [Authorize(Roles = "Admin,ProductCategories")]
        // GET: ProductCategories
        public ActionResult Index()
        {
            ViewBag.MainCategory = new SelectList(_db.ProductMainCategorys.GetDdl(), "value", "label");
            return View();
        }

        public JsonResult GetAll(DataRequest request, int[] filter)
        {
            var result = _db.ProductCategorys.DT_CatgoryWise(request, filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "Admin,ProductCategories")]
        // GET: ProductCategories/Create
        public ActionResult Create()
        {
            ViewBag.ProductMainCategoryID = new SelectList(_db.ProductMainCategorys.GetDdl(), "value", "label");
            var model = new ProductCategory();
            return View("_Create", model);
        }

        // POST: ProductCategories/Create
        [HttpPost]
        public async Task<ActionResult> Create(ProductCategory model)
        {
            ViewBag.ProductMainCategoryID = new SelectList(_db.ProductMainCategorys.GetDdl(), "value", "label", model.ProductMainCategoryID);

            var exist = _db.ProductCategorys.Any(n => n.ProductCategoryName == model.ProductCategoryName && n.ProductMainCategoryID == model.ProductMainCategoryID);
            if (exist) ModelState.AddModelError("ProductCategoryName", "Sub-Category Name already exist!");

            if (!ModelState.IsValid) return View("_Create", model);

            _db.ProductCategorys.Add(model);
            var task = await _db.SaveChangesAsync();
            if (task != 0) return Content("success");

            ModelState.AddModelError("", "Unable to insert record!");
            return View("_Create", model);
        }

        [Authorize(Roles = "Admin,ProductCategories")]
        // GET: ProductCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var productCategory = _db.ProductCategorys.Find(id.GetValueOrDefault());
            ViewBag.ProductMainCategoryID = new SelectList(_db.ProductMainCategorys.GetDdl(), "value", "label", productCategory.ProductMainCategoryID);

            if (Request.IsAjaxRequest()) return PartialView("_Edit", productCategory);

            return View(productCategory);
        }

        // POST: ProductCategories/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ProductCategory model)
        {
            ViewBag.ProductMainCategoryID = new SelectList(_db.ProductMainCategorys.GetDdl(), "value", "label", model.ProductMainCategoryID);

            var exist = _db.ProductCategorys.Any(n => n.ProductCategoryName == model.ProductCategoryName && n.ProductCategoryID != model.ProductCategoryID);
            if (exist) ModelState.AddModelError("ProductCategoryName", "Sub-Category Name already exist!");

            if (!ModelState.IsValid) return View(Request.IsAjaxRequest() ? "_Edit" : "Edit", model);

            _db.ProductCategorys.Update(model);
            var task = await _db.SaveChangesAsync();

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


        // POST: ProductCategories/Delete/5
        public int Delete(int id)
        {
            var productCategory = _db.ProductCategorys.Find(id);
            _db.ProductCategorys.Remove(productCategory);
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
