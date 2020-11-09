using ORDER_MANAGEMENT.Data;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "Admin,EquipmentDistribution")]
    public class EquipmentDistributionController : Controller
    {
        private readonly IUnitOfWork _db;
        public EquipmentDistributionController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        // GET: EquipmentDistribution
        public ActionResult Distribution()
        {
            return View();
        }

        // POST: EquipmentDistribution
        [HttpPost]
        public ActionResult Distribution(EquipmentDistributionVM model)
        {
            model.AssignByRegistrationID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            if (!ModelState.IsValid) return View(model);

            _db.EquipmentDistributions.Distribution(model);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Distribution Successfully!";

            return RedirectToAction("Distribution");
        }

        public JsonResult FindOutlet(string prefix)
        {
            var data = _db.Outlets.Search(prefix);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FindEquipment(string prefix)
        {
            var data = _db.Equipments.Search(prefix);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
