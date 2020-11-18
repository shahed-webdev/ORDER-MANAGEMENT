using Newtonsoft.Json;
using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = ("User"))]
    public class UserDashboardController : Controller
    {
        private readonly IUnitOfWork _db;
        public UserDashboardController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        // GET: UserDashboard
        public ActionResult Index()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var user = _db.Users.GetDashUser(id);
            user.ReportTo = _db.Users.GetReportTo(id);
            user.ReportFrom = _db.Users.GetReportFrom(id);
            user.TargetReport = _db.Users.GetTargetReport(id);
            return View(user);
        }

        //Profile update
        public ActionResult UserProfile()
        {
            var user = _db.Users.GetUserDetails(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                var registration = _db.Registrations.Reg_Update(User.Identity.Name, user);
                _db.Registrations.Update(registration);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }


        public ActionResult Tracking()
        {
            return View();
        }


        //Login Info
        public string GetAdminBasic()
        {
            var admin = _db.Registrations.GetAdminBasic(User.Identity.Name);
            return JsonConvert.SerializeObject(admin);
        }
    }
}