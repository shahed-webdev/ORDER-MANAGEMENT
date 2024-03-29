﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using ORDER_MANAGEMENT.API.Models;
using ORDER_MANAGEMENT.Data;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ORDER_MANAGEMENT.API.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUnitOfWork _db;
        public UserController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }
        [HttpGet]
        // GET: api/TargetInfo
        [Route("api/TargetInfo")]
        public TargetInfo TargetInfo()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var t_info = _db.Users.API_TargetInfo(id);
            return t_info;
        }

        // GET: api/getUserInfo
        [Route("api/getUserInfo")]
        public UserNameRank getUserInfo()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var user = _db.Users.API_getUserInfo(id);
            return user;
        }

        // GET: api/getUserInfo
        [Route("api/getReportTo")]
        public UserReportTo getReportTo()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            return _db.Users.GetReportTo(id);
        }

        // GET: api/getUserInfo
        [Route("api/getTerritory")]
        public List<DDL> getTerritory()
        {
            var id = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            return _db.Territorys.GetUserTerritory(id);
        }


        [HttpPost]
        [Route("api/UserProfileUpdate")]
        public IHttpActionResult UserProfileUpdate([FromBody] UserInfoUpdate value)
        {
            if (value == null) NotFound();

            var RegID = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            var Reg = _db.Users.UserInfoUpdate(RegID, value);
            _db.Registrations.Update(Reg);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var result = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (result.Succeeded) return Ok();

            return Content(HttpStatusCode.InternalServerError, result);
        }

        [HttpPost]
        [Route("api/CheckActivation")]
        public IHttpActionResult CheckActivation()
        {
            var isDeactivated = _db.Registrations.IsDeactivated(User.Identity.Name);

            if (isDeactivated) Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);

            return Ok(isDeactivated);
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
    }
}
