﻿using Microsoft.AspNet.Identity.Owin;
using ORDER_MANAGEMENT.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize]
    public class SubAdminController : Controller
    {
        private readonly IUnitOfWork _db;
        public SubAdminController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [Authorize(Roles = "Admin,SubAdmin_List")]
        // GET: SubAdmin
        public ActionResult Index()
        {
            var model = _db.Registrations.GetSubAdminList();
            return View(model);
        }

        [Authorize(Roles = "Admin,SubAdmin_Page Access")]
        // GET:  Page Access
        public ActionResult PageAccess()
        {
            ViewBag.SubAdmins = new SelectList(_db.Registrations.SubAdmins(), "value", "label");
            var model = _db.PageLinkAssigns.SubAdminLinks(0);
            return View(model);
        }

        public ActionResult GetLinks(int regId)
        {
            var model = _db.PageLinkAssigns.SubAdminLinks(regId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public async Task<bool> PostLinks(int regId, ICollection<PageAssignVM> links)
        {
            try
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var userName = _db.PageLinkAssigns.AssignLink(regId, links);
                _db.SaveChanges();

                var user = await userManager.FindByNameAsync(userName);

                var roleList = links.Select(l => l.RoleName).ToList();
                roleList.Add("Sub-admin");

                var userRoles = await userManager.GetRolesAsync(user.Id);
                await userManager.RemoveFromRolesAsync(user.Id, userRoles.ToArray());
                await userManager.AddToRolesAsync(user.Id, roleList.ToArray());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
