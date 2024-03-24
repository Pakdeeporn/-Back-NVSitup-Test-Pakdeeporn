using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApis.Interface;
using WebApis.Models;

namespace WebApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly IUserRepository _iUser;

        public DemoController(IUserRepository iUser)
        {
            _iUser = iUser;
        }

        [HttpPut]
        [Route("InitData")]
        public ActionResult InitData()
        {
            return Ok(_iUser.CreateMockData());
        }

        [HttpGet]
        [Route("GetUserById")]
        public ActionResult GetUserById(string user_id)
        {
            return Ok(_iUser.GetUserById(user_id));
        }

        [HttpGet]
        [Route("GetListUser")]
        public ActionResult GetListUser()
        {
            return Ok(_iUser.GetListUser());
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult CreateUser([FromBody] User user)
        {
            return Ok(_iUser.CreateUser(user));
        }

        [HttpPut]
        [Route("UpdateUser")]
        public ActionResult UpdateUser([FromBody] User user)
        {
            return Ok(_iUser.UpdateUser(user));
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult DeleteUser(string user_id)
        {
            return Ok(_iUser.DeleteUser(user_id));
        }

        [HttpGet]
        [Route("GetListUserTable")]
        public ActionResult GetListUserTable()
        {
            return Ok(_iUser.GetListUserTable());
        }

    }
}