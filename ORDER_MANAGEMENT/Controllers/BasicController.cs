using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using ORDER_MANAGEMENT.Data;
using ORDER_MANAGEMENT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class BasicController : Controller
    {
        private readonly IUnitOfWork _db;

        readonly RoleManager<IdentityRole> _roleManager;
        public BasicController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
            var context = new ApplicationDbContext();
            var roleStore = new RoleStore<IdentityRole>(context);
            _roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        //Get Region
        [Authorize(Roles = "Admin, Sub-admin")]
        public ActionResult GetRegionDDL()
        {
            var list = _db.Regions.GetDllRegion();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //Get Area by Region
        [Authorize(Roles = "Admin, Sub-admin")]
        public ActionResult GetAreaByRegion(string Ids)
        {
            var serializer = new JavaScriptSerializer();
            var RegionIds = serializer.Deserialize<List<int>>(Ids);

            var list = _db.Areas.GetDll_AreaByRegion(RegionIds);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //Get Territory by Area
        public ActionResult GetTerritoryTable(string Ids)
        {
            var serializer = new JavaScriptSerializer();
            var AreaIds = serializer.Deserialize<List<int>>(Ids);

            var list = _db.Territorys.GetTerritory(AreaIds);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //User Tracking
        [Authorize(Roles = "Admin, Sub-admin, User")]
        public string TrackingByUser(int RegID, DateTime date)
        {
            var list = _db.UserTrackingByDistributors.TrackingUserWise(RegID, date);
            var Outletlist = _db.UserTrackingByOutlets.TrackingUserWise(RegID, date);

            list.AddRange(Outletlist);
            list.OrderBy(l => l.CheckInTime);
            return JsonConvert.SerializeObject(list);
        }


        //Login Info
        [Authorize(Roles = "Admin, Sub-admin, User")]
        public string GetUserLogedInInfo()
        {
            var admin = _db.Registrations.GetAdminBasic(User.Identity.Name);
            return JsonConvert.SerializeObject(admin);
        }


        //Side Menu
        [Authorize(Roles = "Admin, Sub-admin")]
        public string GetSideManu()
        {
            var data = _db.PageLinks.GetSideMenuByUser(User.Identity.Name);
            return JsonConvert.SerializeObject(data);
        }


        /******PAGE ACCESS ROLE********/
        public ActionResult PageRole()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        //GET
        public ActionResult CreateRole()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult CreateRole(IdentityRole role)
        {
            if (role == null) return View();
            if (_roleManager.RoleExists(role.Name)) return View();
            _roleManager.Create(role);

            return RedirectToAction("PageRole");
        }

        public bool DeleteRole(string roleName)
        {
            if (roleName == null) return false;

            var role = _roleManager.FindByName(roleName);
            if (role.Users.Count > 0) return false;
            var r = _roleManager.Delete(role);
            return r.Succeeded;
        }

        // GET:  Page Link
        public ActionResult PageLink()
        {
            ViewBag.roleList = _roleManager.Roles.Select(r => new RoleDDL { RoleId = r.Id, Role = r.Name });

            var model = _db.PageLinkCategorys.GetCategoryWithLink();
            return View(model);
        }
        public ActionResult CreateLinks()
        {
            ViewBag.roleList = _roleManager.Roles.Select(r => new RoleDDL { RoleId = r.Id, Role = r.Name }).ToList();
            ViewBag.Category = _db.PageLinkCategorys.ddl();
            return View();
        }

        [HttpPost]
        public ActionResult CreateLinks(PageLink model)
        {
            if (!ModelState.IsValid) return View();

            _db.PageLinks.Add(model);
            _db.SaveChanges();

            return RedirectToAction("PageLink");
        }

        public bool PageLinkUpdate(int LinkID, Guid RoleId)
        {
            var pagelink = _db.PageLinkCategorys.LinkRoleUpdate(LinkID, RoleId);
            _db.PageLinks.Update(pagelink);
            var r = _db.SaveChanges();

            return r > 0 ? true : false;
        }
    }
}