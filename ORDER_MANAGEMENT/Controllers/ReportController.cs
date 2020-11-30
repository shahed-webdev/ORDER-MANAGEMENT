using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ORDER_MANAGEMENT.Data;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _db;

        public ReportController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: Order Report
        [Authorize(Roles = "Admin, OrderReport")]
        public ActionResult OrderReport()
        {
            ViewBag.Region = new SelectList(_db.Regions.GetAll(), "RegionID", "RegionName");
            return View();
        }

        public ActionResult GetOrderReport(OutletReportFilterModel filterModel)
        {
            var response = _db.OutletOrders.OrderReport(filterModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //get dropdown value
        public ActionResult AreaByRegion(int regionId)
        {
            var ids = new List<int> {regionId};
            var list = _db.Areas.GetAreaByRegion(ids);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TerritoryByArea(int areaId)
        {
            var ids = new List<int> { areaId };
            var list = _db.Territorys.GetTerritory(ids);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DepotByRegion(int regionId)
        {
            var list = _db.Depots.Ddls(regionId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}