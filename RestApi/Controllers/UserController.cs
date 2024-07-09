using Microsoft.AspNetCore.Mvc;
using Model;
using Model.ModelDto.UserDto;


namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBusinessLogic businessLogic;

        public UserController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginRequest userLoginRequest)
        {
            var user = businessLogic.Login(userLoginRequest.Username, userLoginRequest.Password);

            if(user == null)
            {
                return Unauthorized(user);
            }
            var userDto = new UserLoginResponse();
            userDto.UserId = user.UserId;
            userDto.Username = user.Username;
            userDto.Role = user.Role;

            return Ok(userDto);
        }

        [HttpGet]

        public IActionResult GetUserByUsername(string username)
        {
            var user = businessLogic.GetUserByUserName(username);

            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserByUsernameResponse();

            userDto.UserId= user.UserId;
            userDto.Username = user.Username;

            return Ok(userDto);
        }
    }
}
