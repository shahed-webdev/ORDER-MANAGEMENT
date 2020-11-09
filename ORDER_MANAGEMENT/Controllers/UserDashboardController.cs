using Newtonsoft.Json;
using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = ("User"))]
    public class UserDashboardController : Controller
    {
        private readonly IUnitOfWork db;
        public UserDashboardController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        // GET: UserDashboard
        public ActionResult Index()
        {
            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var user = db.Users.GetDashUser(id);
            user.ReportTo = db.Users.GetReportTo(id);
            user.ReportFrom = db.Users.GetReportFrom(id);
            user.TargetReport = db.Users.GetTargetReport(id);
            return View(user);
        }

        //Profile update
        public ActionResult UserProfile()
        {
            var user = db.Users.GetUserDetails(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                var registration = db.Registrations.Reg_Update(User.Identity.Name, user);
                db.Registrations.Update(registration);
                db.SaveChanges();

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
            var admin = db.Registrations.GetAdminBasic(User.Identity.Name);
            return JsonConvert.SerializeObject(admin);
        }


    }
}