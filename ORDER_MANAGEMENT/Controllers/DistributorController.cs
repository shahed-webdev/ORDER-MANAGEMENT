using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using ORDER_MANAGEMENT.Data;
using ORDER_MANAGEMENT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class DistributorController : Controller
    {
        private readonly IUnitOfWork _db;
        public DistributorController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        // GET: Distributor
        [Authorize(Roles = ("Admin,Distributor_List"))]
        public ActionResult Index()
        {
            ViewBag.RegionID = new SelectList(_db.Regions.GetDllRegion(), "RegionID", "RegionName");
            var model = _db.Distributors.DistributorListWithUser();
            return View(model);
        }

        //assign to depot
        public ActionResult AssignToDepot(int distributorId, int depotId)
        {
            _db.Distributors.AssignDepot(distributorId, depotId);
            _db.SaveChanges();

            return Content("success");
        }


        [Authorize(Roles = ("Admin,ChangeDiscount"))]
        // GET: ChangeDiscount
        public ActionResult ChangeDiscount()
        {
            return View();
        }

        public int DiscountPercentageChange(int TerritoryID, double Percentage)
        {
            _db.Territorys.DistributorPercentageChange(TerritoryID, Percentage);
            return _db.SaveChanges();
        }

        [Authorize(Roles = ("Admin,MapLocation"))]
        // GET: MapLocation
        public ActionResult MapLocation()
        {
            return View();
        }
      
        public string GetDistributorList()
        {
            var list = _db.Distributors.DistributorList();
            return JsonConvert.SerializeObject(list);
        }

        [Authorize(Roles = ("Admin,Distributor_List"))]
        // GET: Approved
        public ActionResult Approved(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var distributorId = id.GetValueOrDefault();
            var dis = _db.Distributors.Where(d => d.DistributorID == distributorId && !d.IsApproved).FirstOrDefault();

            if (dis == null) return RedirectToAction("Index");

            var dr = new DistirubtorRegisterVM {DistributorID = dis.DistributorID, Name = dis.Name, Phone = dis.Mobile};

            return View(dr);
        }

        // POST: Approved
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approved(DistirubtorRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var SignUpUser = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = UserManager.Create(SignUpUser, model.Password);

                if (result.Succeeded)
                {
                    UserManager.AddToRole(SignUpUser.Id, "Distributor");

                    Registration Reg = new Registration()
                    {
                        Type = "Distributor",
                        Name = model.Name,
                        UserName = model.UserName,
                        PersonalEmail = model.Email,
                        PresentAddress = model.Address,
                        PS = model.Password
                    };

                    _db.Registrations.Add(Reg);

                    var ApproveBy_RegistrationID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
                    _db.Distributors.Approved(ApproveBy_RegistrationID, model, Reg);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("UserName", error);
                    }
                }
            }

            return View(model);
        }

        [Authorize(Roles = ("Admin,Distributor_List"))]
        // GET: Update
        public ActionResult Update(int? id)
        {

            var Dis = _db.Distributors.GetDetails(id);
            return View(Dis);
        }

        // POST: Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(DistributorDetails model)
        {
            if (ModelState.IsValid)
            {
                _db.Distributors.UpdateDetails(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = ("Admin,OrderReturn"))]
        // GET: OrderReturn
        public ActionResult OrderReturn()
        {
            var model = _db.DistributorProductReturns.ApprovedReturnPendingList();
            return View(model);
        }

        [Authorize(Roles = ("Admin,OrderReturn"))]
        // GET: ReturnDetails
        public ActionResult ReturnDetails(int? id)
        {
            var ordermodel = _db.DistributorProductReturns.ApprovedReturnPendingList();
            if (id == null) return RedirectToAction("OrderReturn", ordermodel);

            var model = _db.DistributorProductReturns.ApprovedReturnOrderDetails(id.GetValueOrDefault());
            return View(model);
        }

        // POST: Return Approved
        public int ReturnApproved(int Id, double ReturnAmount)
        {
            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            _db.DistributorProductReturns.ApprovedReturn(Id, RegID, ReturnAmount);

            return _db.SaveChanges();
        }

        // POST: Return Reject 
        public int RejectReturn(int Id)
        {
            _db.DistributorProductReturns.RejectReturn(Id);
            return _db.SaveChanges();
        }

        [Authorize(Roles = ("Admin,AssignSR"))]
        // GET: Assign SR
        public ActionResult AssignSR()
        {
            return View();
        }

        //Get: Distributor ddl
        public ActionResult GetDistributorddlList(string Ids)
        {
            var serializer = new JavaScriptSerializer();
            var TerritoryIds = serializer.Deserialize<List<int>>(Ids);

            var list = _db.Distributors.DistributorByTerritorys(TerritoryIds);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //Get: Territory
        public ActionResult GetTerritorybyDistributor(int Id)
        {
            var list = _db.Users.GetSR_ByDistributorTerritory(Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //Post: Assign
        public int AssignTerritory(int Id, string Ids)
        {
            var serializer = new JavaScriptSerializer();
            var srIds = serializer.Deserialize<List<int>>(Ids);

            _db.Distributors.AssignSR(Id, srIds);
            _db.SaveChanges();

            return _db.SaveChanges();
        }
    }
}
