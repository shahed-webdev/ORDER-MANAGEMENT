using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = ("Admin,Sub-admin"))]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork db;
        public DashboardController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        // GET: Deshboard
        public ActionResult Index()
        {
            var data = new AdminDashboardViewModel(db);
            return View(data);
        }

        //Profile update
        public ActionResult UserProfile()
        {
            var user = db.Registrations.GetAdminInfo(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(UserDetails user)
        {
            if (!ModelState.IsValid) return View(user);

            var registration = db.Registrations.Reg_Update(User.Identity.Name, user);
            db.Registrations.Update(registration);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
