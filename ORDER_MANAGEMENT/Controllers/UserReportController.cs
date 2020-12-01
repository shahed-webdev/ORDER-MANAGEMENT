using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ORDER_MANAGEMENT.Data;

namespace ORDER_MANAGEMENT.Controllers
{
    public class UserReportController : Controller
    {
        private readonly IUnitOfWork _db;

        public UserReportController(IUnitOfWork db)
        {
            _db = db;
        }

        // GET: UserReport
        public ActionResult Index()
        {
            return View();
        }
    }
}