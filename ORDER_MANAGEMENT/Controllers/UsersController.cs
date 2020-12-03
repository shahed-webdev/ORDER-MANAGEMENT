using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ORDER_MANAGEMENT.Data;
using ORDER_MANAGEMENT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _db;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [Authorize(Roles = "Admin,User_List")]
        // GET: Users
        public ActionResult Index()
        {
            return View(_db.Users.GetAllUser());
        }

        //Deactivate User Login
        public ActionResult DeactivateUserLogin(int id)
        {
            var response = _db.Registrations.ToggleActivation(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Dll_user(int rank, int regId = 0)
        {
            var list = _db.Users.GetUsersDLLByHierarchy(rank, regId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserDDL()
        {
            var list = _db.Users.GetUserDDL();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dll_region(int registrationId)
        {
            var list = _db.Regions.GetUserRegion(registrationId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dll_area(int registrationId, string ids)
        {
            var serializer = new JavaScriptSerializer();
            var AreaIds = serializer.Deserialize<List<int>>(ids);

            var list = _db.Areas.GetUserArea(registrationId, AreaIds);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dll_territory(int upperRegistrationId, string ids, int registrationId = 0)
        {
            var serializer = new JavaScriptSerializer();
            var AreaIDs = serializer.Deserialize<List<int>>(ids);

            var list = _db.Territorys.GetUserUnAsignTerritory(upperRegistrationId, AreaIDs, registrationId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        // GET: Users/Create
        public ActionResult Create()
        {
            var repo = new CreateUserVM();
            return View(repo);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserVM userVM)
        {
            var context = new ApplicationDbContext();

            const string password = "Pw_123456";
            userVM.PS = password;

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var signUpUser = new ApplicationUser { UserName = userVM.UserName, Email = userVM.PersonalEmail };
            var result = userManager.Create(signUpUser, password);

            if (result.Succeeded)
            {
                if (!ModelState.IsValid) return View(userVM);

                userManager.AddToRole(signUpUser.Id, "User");

                _db.Users.CreateUser(userVM);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("UserName", error);
            }

            return View(userVM);
        }

        // GET: UpdateUsers
        public ActionResult UpdateUsers(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var user = _db.Users.UpdateDetails(id.GetValueOrDefault());

            user.Hierarchies = new SelectList(_db.Hierarchys.GetDll_Hierarchy(), "Rank", "HierarchyName");
            user.Regions = _db.Regions.GetUserRegion(user.UpperRegistrationID).Select(r => new SelectListItem { Value = r.RegionID.ToString(), Text = r.RegionName });
            user.RegionIDs = _db.Regions.GetUserRegion(id.GetValueOrDefault()).Select(r => r.RegionID).ToArray();
            user.Areas = _db.Areas.GetUserArea(user.UpperRegistrationID, user.RegionIDs.ToList()).Select(a => new SelectListItem { Value = a.AreaID.ToString(), Text = a.AreaName });
            user.AreaIDs = _db.Areas.GetUserArea(id.GetValueOrDefault(), user.RegionIDs.ToList()).Select(a => a.AreaID).ToArray();
            user.Territorys = _db.Territorys.GetUserUnAsignTerritory(user.UpperRegistrationID, user.AreaIDs.ToList(), id.GetValueOrDefault()).Select(t => new SelectListItem() { Value = t.TerritoryID.ToString(), Text = t.TerritoryName });
            user.TerritoryIDs = _db.Territorys.GetUserTerritory(id.GetValueOrDefault(), user.AreaIDs.ToList()).Select(t => t.TerritoryID).ToArray();

            return View(user);
        }

        // POST: UpdateUsers/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUsers(UpdateUserVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var reg = _db.Users.ChangeHierarchyTerritory(model);
            _db.Registrations.Update(reg);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: Users/Delete/5
        public int Delete(int id)
        {
            var user = _db.Users.Find(id);

            _db.Users.Remove(user);
            return _db.SaveChanges();
        }

        [Authorize(Roles = "Admin,UserTracking")]
        //UserTracking
        public ActionResult UserTracking()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
