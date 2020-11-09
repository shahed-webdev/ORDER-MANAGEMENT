using ORDER_MANAGEMENT.Data;
using System.Collections.Generic;
using System.Web.Http;

namespace ORDER_MANAGEMENT.API.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUnitOfWork db;
        public UserController(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }
        [HttpGet]
        // GET: api/TargetInfo
        [Route("api/TargetInfo")]
        public TargetInfo TargetInfo()
        {
            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var t_info = db.Users.API_TargetInfo(id);
            return t_info;
        }

        // GET: api/getUserInfo
        [Route("api/getUserInfo")]
        public UserNameRank getUserInfo()
        {
            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            var U = db.Users.API_getUserInfo(id);
            return U;
        }

        // GET: api/getUserInfo
        [Route("api/getReportTo")]
        public UserReportTo getReportTo()
        {
            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            return db.Users.GetReportTo(id);
        }

        // GET: api/getUserInfo
        [Route("api/getTerritory")]
        public List<DDL> getTerritory()
        {
            var id = db.Registrations.GetRegID_ByUserName(User.Identity.Name);
            return db.Territorys.GetUserTerritory(id);
        }


        [HttpPost]
        [Route("api/UserProfileUpdate")]
        public IHttpActionResult UserProfileUpdate([FromBody]UserInfoUpdate value)
        {
            if (value == null) NotFound();

            var RegID = db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            var Reg = db.Users.UserInfoUpdate(RegID, value);
            db.Registrations.Update(Reg);
            db.SaveChanges();

            return Ok();
        }
    }
}
