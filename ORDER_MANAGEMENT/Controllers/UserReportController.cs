using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ORDER_MANAGEMENT.Data;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class UserReportController : Controller
    {
        private readonly IUnitOfWork _db;

        public UserReportController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: User Report
        [Authorize(Roles = "Admin, UserOrderReport")]
        public ActionResult OrderReport()
        {
            ViewBag.Hierarchy = new SelectList(_db.Hierarchys.GetDll_Hierarchy(), "Rank", "HierarchyName");
            return View();
        }

        [HttpPost]
        public ActionResult GetOrderReport(UserReportFilterModel filter)
        {
            var response = _db.Users.OrderReport(filter);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //GET:Sales Report
        [Authorize(Roles = "Admin, UserSalesReport")]
        public ActionResult SalesReport()
        {
            ViewBag.Hierarchy = new SelectList(_db.Hierarchys.GetDll_Hierarchy(), "Rank", "HierarchyName");
            return View();
        }
        
        [HttpPost]
        public ActionResult GetSalesReport(UserReportFilterModel filter)
        {
            var response = _db.Users.SalesReport(filter);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //GET: Revenue Report
        [Authorize(Roles = "Admin, UserRevenueReport")]
        public ActionResult RevenueReport()
        {
            ViewBag.Hierarchy = new SelectList(_db.Hierarchys.GetDll_Hierarchy(), "Rank", "HierarchyName");
            return View();
        }
       
        [HttpPost]
        public ActionResult GetRevenueReport(UserReportFilterModel filter)
        {
            var response = _db.Users.RevenueReport(filter);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //GET:Target Vs Achieved Report
        [Authorize(Roles = "Admin, UserTargetAchievedReport")]
        public ActionResult TargetAchievedReport()
        {
            ViewBag.Hierarchy = new SelectList(_db.Hierarchys.GetDll_Hierarchy(), "Rank", "HierarchyName");
            return View();
        }

        [HttpPost]
        public ActionResult GetTargetAchievedReport(int id)
        {
            var response = _db.Users.TargetVsAchievedReport(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //dropdown lists
        public ActionResult GetUserByRank(int rank)
        {
            var response = _db.Users.GetUserByRankDdl(rank);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}