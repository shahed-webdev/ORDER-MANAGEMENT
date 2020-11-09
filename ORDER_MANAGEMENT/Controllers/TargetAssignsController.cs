using ORDER_MANAGEMENT.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ORDER_MANAGEMENT.Controllers
{
    [Authorize(Roles = "User")]
    public class TargetAssignsController : Controller
    {
        private readonly IUnitOfWork db;
        public TargetAssignsController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public ActionResult ddl_UserTargetAmount(int TargetID, int RegistrationID)
        {
            var list = db.Targets.ddl_UserTargetAmount(TargetID, RegistrationID, db.Registrations.GetRegID_ByUserName(User.Identity.Name));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: TargetAssigns/Create
        public ActionResult Create(int? id)
        {
            if (id == null) return RedirectToAction("Index", "UserDashboard");

            var model = new TargetAssignVM();
            var loginUserId = db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            model.TargetReport = db.Users.GetTargetReport(loginUserId);
            model.user = db.Users.GetUserInfo(id.GetValueOrDefault());
            model.Target_Ddl = db.Targets.TargetAssignddl(loginUserId);

            return View(model);
        }

        // POST: TargetAssigns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TargetAssignCreateVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Is_New_Target)
                {
                    var TA = new TargetAssign();
                    TA.TargetAmount = model.TargetAmount;
                    TA.TargetID = model.TargetID;
                    TA.RegistrationID = model.RegistrationID;
                    TA.AssignByRegistrationID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
                    db.TargetAssigns.Add(TA);
                }
                else
                {
                    var ta = db.TargetAssigns.Where(t => t.RegistrationID == model.RegistrationID && t.TargetID == model.TargetID).FirstOrDefault();
                    ta.TargetAmount = model.TargetAmount;
                    db.TargetAssigns.Update(ta);
                }

                db.SaveChanges();

                return RedirectToAction("Index", "UserDashboard");
            }


            return View(model);
        }

        // GET: TargetAssigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAssign targetAssign = db.TargetAssigns.Find(id.GetValueOrDefault());
            if (targetAssign == null)
            {
                return HttpNotFound();
            }

            return View(targetAssign);
        }

        // POST: TargetAssigns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TargetAssign targetAssign)
        {
            if (ModelState.IsValid)
            {
                db.TargetAssigns.Update(targetAssign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(targetAssign);
        }

        // GET: TargetAssigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetAssign targetAssign = db.TargetAssigns.Find(id.GetValueOrDefault());
            if (targetAssign == null)
            {
                return HttpNotFound();
            }
            return View(targetAssign);
        }

        // POST: TargetAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TargetAssign targetAssign = db.TargetAssigns.Find(id);
            db.TargetAssigns.Remove(targetAssign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
