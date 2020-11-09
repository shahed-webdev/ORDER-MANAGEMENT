using ORDER_MANAGEMENT.Data;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _db;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [Authorize(Roles = "Admin,Products_List")]
        // GET: Products
        public ActionResult Index()
        {
            ViewBag.MainCategory = new SelectList(_db.ProductMainCategorys.GetDdlforSub(), "value", "label");
            return View();
        }


        public JsonResult GetSubCategory(int id)
        {
            var list = _db.ProductCategorys.GetCategoryByMainDDL(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategoryProduct(DataRequest request, int[] filter)
        {
            var result = _db.Products.DT_GetCategoryProducts(request, filter);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public int AddQuantity(int productId, int quantity)
        {
            var registrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            _db.Products.AddQuantity(productId, registrationId, quantity);
            return _db.SaveChanges();
        }

        public ICollection<Product> LowStock(int limit)
        {
            return _db.Products.LowStock(limit);
        }

        [Authorize(Roles = "Admin,Products_List")]
        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.MainCategory = new SelectList(_db.ProductMainCategorys.GetDdlforSub(), "value", "label");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM model)
        {
            ViewBag.MainCategory = new SelectList(_db.ProductMainCategorys.GetDdlforSub(), "value", "label");

            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ProductCategoryID = model.ProductCategoryID,
                    ProductName = model.ProductName,
                    MRP = model.MRP,
                    ProductCode = model.ProductCode,
                    SKU = model.SKU,
                    Size = model.Size,
                    Description = model.Description,
                    ProductImage = model.ProductImage
                };


                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(_db.ProductCategorys.GetAll(), "ProductCategoryID", "ProductCategoryName", model.ProductCategoryID);
            return View(model);
        }

        [Authorize(Roles = "Admin,Products_List")]
        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _db.Products.Find(id.GetValueOrDefault());

            var model = new ProductVM
            {
                ProductID = product.ProductID,
                ProductCategoryID = product.ProductCategoryID,
                ProductName = product.ProductName,
                MRP = product.MRP,
                ProductCode = product.ProductCode,
                SKU = product.SKU,
                Size = product.Size,
                Description = product.Description,
                ProductImage = product.ProductImage
            };

            var productMainCategoryID = _db.ProductCategorys.Find(model.ProductCategoryID).ProductMainCategoryID;
            ViewBag.MainCategory = new SelectList(_db.ProductMainCategorys.GetDdlforSub(), "value", "label", productMainCategoryID);
            ViewBag.ProductCategoryID = new SelectList(_db.ProductCategorys.GetCategoryByMainDDL(productMainCategoryID), "value", "label", product.ProductCategoryID);

            return View(model);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductVM model)
        {
            if (ModelState.IsValid)
            {
                var product = _db.Products.Find(model.ProductID);

                product.ProductCategoryID = model.ProductCategoryID;
                product.ProductName = model.ProductName;
                product.MRP = model.MRP;
                product.ProductCode = model.ProductCode;
                product.SKU = model.SKU;
                product.Size = model.Size;
                product.Description = model.Description;
                product.ProductImage = model.ProductImage;

                _db.Products.Update(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            var productMainCategoryID = _db.ProductCategorys.Find(model.ProductCategoryID).ProductMainCategoryID;
            ViewBag.MainCategory = new SelectList(_db.ProductMainCategorys.GetDdlforSub(), "value", "label", productMainCategoryID);
            ViewBag.ProductCategoryID = new SelectList(_db.ProductCategorys.GetCategoryByMainDDL(productMainCategoryID), "value", "label", model.ProductCategoryID);

            return View(model);
        }

        [Authorize(Roles = "Admin,Distributor_OrderList")]
        //Order List
        public ActionResult Order_List()
        {
            var list = _db.DistributorOrders.UnapprovedOrderList();
            return View(list);
        }

        [Authorize(Roles = "Admin,Distributor_OrderList")]
        //Approve product
        public ActionResult ApproveProduct(int? id)
        {
            var list = _db.DistributorOrders.UnapprovedOrderList();
            if (id == null) return RedirectToAction("Order_List", list);

            var order = _db.DistributorOrders.OrderApprovedDetails(id.GetValueOrDefault());
            return View(order);
        }

        //Post Approve product
        [HttpPost]
        public ActionResult ApproveProduct(DistributorOrderApproved model)
        {
            if (!ModelState.IsValid) return View(model);

            var registrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.DistributorOrders.OrderConfirm(model, registrationId);
            _db.SaveChanges();

            _db.DistributorOrders.OrderTargetUpdate(model.OrderInfo.DistributorID, model.OrderInfo.DistributorOrderID);
            _db.SaveChanges();

            return RedirectToAction("Order_List");
        }

        // POST: Products/Delete/5
        public int Delete(int id)
        {
            var product = _db.Products.Find(id);
            _db.Products.Remove(product);
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
