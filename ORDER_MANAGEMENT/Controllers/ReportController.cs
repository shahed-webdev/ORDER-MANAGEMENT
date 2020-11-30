using System;
using System.Collections;
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

        [HttpPost]
        public ActionResult GetOrderReport(OutletReportFilterModel filterModel)
        {
            var response = _db.OutletOrders.OrderReport(filterModel);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        // GET: Sales Report
        [Authorize(Roles = "Admin, SalesReport")]
        public ActionResult SalesReport()
        {
            ViewBag.Region = new SelectList(_db.Regions.GetAll(), "RegionID", "RegionName");
            return View();
        }

        [HttpPost]
        public ActionResult GetSalesReport(OutletReportFilterModel filter)
        {
            var response = _db.OutletOrders.SalesReport(filter);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        // GET: revenue Report
        [Authorize(Roles = "Admin, RevenueReport")]
        public ActionResult RevenueReport()
        {
            ViewBag.Region = new SelectList(_db.Regions.GetAll(), "RegionID", "RegionName");
            return View();
        }

        [HttpPost]
        public ActionResult GetRevenueReport(OutletReportFilterModel filter)
        {
            var response = _db.OutletOrders.RevenueReport(filter);
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
        
        public ActionResult DistributorByTerritory(string ids)
        {
            var serializer = new JavaScriptSerializer();
            var territoryIds = serializer.Deserialize<List<int>>(ids);

            var list = _db.Distributors.DistributorByTerritorys(territoryIds);
            return Json(list, JsonRequestBehavior.AllowGet);
        } 
        
        public ActionResult OutletByTerritory(string ids)
        {
            var serializer = new JavaScriptSerializer();
            var territoryIds = serializer.Deserialize<List<int>>(ids);

            var list = _db.Outlets.OutletByTerritorys(territoryIds);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}