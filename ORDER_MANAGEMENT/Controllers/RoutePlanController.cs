using ORDER_MANAGEMENT.Data;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class RoutePlanController : Controller
    {
        private readonly IUnitOfWork db;
        public RoutePlanController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        [Authorize(Roles = "Admin,RoutePlan")]
        // GET: Route Plan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RouteUserList()
        {
            var list = db.UserRoutes.RouteUserList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin,Create_RoutePlan")]
        //GET: Assign
        public ActionResult Assign()
        {
            return View();
        }

        // GET: RoutePlan
        public ActionResult CreateRoutePlan()
        {
            return View("_CreateRoutePlan", new RoutePlanMV());
        }

        // POST: RoutePlan
        [HttpPost]
        public async Task<ActionResult> CreateRoutePlan(RoutePlanMV value)
        {
            var RouteNameExsist = db.Routes.Any(n => n.RouteName == value.RouteName);
            if (RouteNameExsist)
            {
                ModelState.AddModelError("RouteName", "Route Name already exsist!");
            }
            if (value.SelectedDays == null)
            {
                ModelState.AddModelError("SelectedDays", "Select Day!");
            }

            if (!ModelState.IsValid) return View("_CreateRoutePlan", value);

            db.Routes.CreateRoute(value);
            var task = await db.SaveChangesAsync();

            if (task == 0)
            {
                ModelState.AddModelError("", "Unable to insert record!");
                return View("_CreateRoutePlan", value);
            }

            return Content("success");
        }

        //Route ddl
        public ActionResult ddlRoutePlan()
        {
            var list = db.Routes.RouteDDL();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //Unassign User ddl
        public ActionResult ddlUnassignUserDdl()
        {
            var list = db.Routes.UnassignRouteUser();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RouteDetails(int Id)
        {
            var list = db.Routes.RouteDetails(Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DistributorDDL(int Id)
        {
            var list = db.Distributors.DistributorByUserTerritory(Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public int AssignDistributors(UserRoutePoint data)
        {
            db.UserRoutes.AddUserInRoute(data);
            var user = db.Users.Find(data.RegistrationID);
            user.RouteID = data.RouteID;
            db.Users.Update(user);

            return db.SaveChanges();
        }


        public ActionResult OutletDDL(int Id)
        {
            var list = db.Outlets.OutletByUserTerritory(Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public int AssignOutlets(UserRoutePoint data)
        {
            db.UserRoutes.AddUserInRoute(data);
            var user = db.Users.Find(data.RegistrationID);
            user.RouteID = data.RouteID;
            db.Users.Update(user);

            return db.SaveChanges();
        }
    }
}
