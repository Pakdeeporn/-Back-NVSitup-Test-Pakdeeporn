using Microsoft.AspNetCore.Mvc;
using WebApis.Interface;

namespace WebApis.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class LoginController : Controller
    {
        private readonly IUserRepository _iUser;

        public LoginController(IUserRepository iUser)
        {
            _iUser = iUser;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string username = "Sara@gmail.com", string password = "123456789")
        {
            return Ok(_iUser.Authenticate(username, password));
        }
    }
}
