using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = ("Admin, Sub-admin"))]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _db;
        public DashboardController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        // GET: Dashboard
        public ActionResult Index()
        {
            var data = new AdminDashboardViewModel(db);
            return View(data);
        }

        //Profile update
        public ActionResult UserProfile()
        {
            var user = _db.Registrations.GetAdminInfo(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserDetails user)
        {
            if (!ModelState.IsValid) return View(user);

            var registration = _db.Registrations.Reg_Update(User.Identity.Name, user);
            _db.Registrations.Update(registration);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
