using Newtonsoft.Json;
using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    public class OutletController : Controller
    {
        private readonly IUnitOfWork db;
        public OutletController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        [Authorize(Roles = "Admin,Outlets")]
        // GET: Outlet
        public ActionResult Index()
        {
            var model = db.Outlets.OutletList();
            return View(model);
        }

        // GET: Distributor/Approved
        public int Approved(int OutletID, int DueRange = 0)
        {
            var RegID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            db.Outlets.Approved(OutletID, RegID, DueRange);

            return db.SaveChanges();
        }

        [Authorize(Roles = "Admin,Outlet_Location Map")]
        //GET: Location Map
        public ActionResult LocationMap()
        {
            return View();
        }

        public string GetOutletList()
        {
            var list = db.Outlets.OutletList();
            return JsonConvert.SerializeObject(list);
        }

        [Authorize(Roles = "Admin,Outlet_OrderList")]
        //GET: OrderList
        public ActionResult OrderList()
        {
            return View();
        }
    }
}
