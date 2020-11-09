using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Distributor")]
    public class DistributorDashboardController : Controller
    {
        // GET: DistributorDashboard

        public ActionResult Index()
        {
            return View();
        }
    }
}
