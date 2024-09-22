using Learning_Dotnet.Dtos;
using Learning_Dotnet.Interfaces;
using Learning_Dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserEFController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserEFController(IConfiguration config, IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _userRepository.GetUsers(pageNo, pageSize);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser([FromRoute] int userId)
        {
            User? foundUser = _userRepository.GetUser(userId);

            if (foundUser == null)
                return NotFound();

            return Ok(foundUser);
        }

        [HttpPost]
        public IActionResult AddUser(UserAddDto user)
        {
            User? foundUser = _userRepository.GetUserByEmail(user.Email);

            if (foundUser != null)
            {
                return Conflict();
            }

            User newUser = _userRepository.CreateUser(user);
            return Ok(newUser);
        }

        [HttpPut("{userId}")]
        public IActionResult EditUser([FromRoute] int userId, [FromBody] User user)
        {
            User? foundUser = _userRepository.UpdateUser(userId, user);

            if (foundUser == null)
                return NotFound();

            return Ok(foundUser);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser([FromRoute] int userId)
        {
            User? foundUser = _userRepository.DeleteUser(userId);

            if (foundUser == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("UserJobInfo")]
        public IEnumerable<UserJobInfo> GetUserJobInfos([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _userRepository.GetUserJobInfos(pageNo, pageSize);
        }

        [HttpGet("UserJobInfo/{userId}")]
        public IActionResult GetUserJobInfo([FromRoute] int userId)
        {
            UserJobInfo? foundUserJobInfo = _userRepository.GetUserJobInfo(userId);

            if (foundUserJobInfo == null)
                return NotFound();

            return Ok(foundUserJobInfo);
        }

        [HttpPost("UserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            User? foundUser = _userRepository.GetUser(userJobInfo.UserId);

            if (foundUser == null)
            {
                return NotFound("User not found");
            }

            UserJobInfo? foundUserJobInfo = _userRepository.GetUserJobInfo(userJobInfo.UserId);

            if (foundUserJobInfo != null)
            {
                return Conflict();
            }

            UserJobInfo newUserJobInfo = _userRepository.CreateUserJobInfo(userJobInfo);
            return Ok(newUserJobInfo);
        }

        [HttpPut("UserJobInfo/{userId}")]
        public IActionResult EditUserJobInfo([FromRoute] int userId, [FromBody] UserJobInfo userJobInfo)
        {
            UserJobInfo? foundUserJobInfo = _userRepository.UpdateUserJobInfo(userId, userJobInfo);

            if (foundUserJobInfo == null)
                return NotFound();

            return Ok(foundUserJobInfo);
        }


        [HttpDelete("UserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo([FromRoute] int userId)
        {
            UserJobInfo? foundUserJobInfo = _userRepository.DeleteUserJobInfo(userId);

            if (foundUserJobInfo == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("UserSalary")]
        public IEnumerable<UserSalary> GetUserSalaries([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return _userRepository.GetUsersSalaries(pageNo, pageSize);
        }

        [HttpGet("UserSalary/{userId}")]
        public IActionResult GetUserSalary([FromRoute] int userId)
        {
            UserSalary? foundUserSalary = _userRepository.GetUserSalary(userId);

            if (foundUserSalary == null)
                return NotFound();

            return Ok(foundUserSalary);
        }

        [HttpPost("UserSalary")]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            User? foundUser = _userRepository.GetUser(userSalary.UserId);

            if (foundUser == null)
            {
                return NotFound("User not found");
            }

            UserSalary? foundUserSalary = _userRepository.GetUserSalary(userSalary.UserId);

            if (foundUserSalary != null)
            {
                return Conflict();
            }

            UserSalary newUserSalary = _userRepository.CreateUserSalary(userSalary);
            return Ok(newUserSalary);
        }

        [HttpPut("UserSalary/{userId}")]
        public IActionResult EditUserSalary([FromRoute] int userId, [FromBody] UserSalary userSalary)
        {
            UserSalary? foundUserSalary = _userRepository.UpdateUserSalary(userId, userSalary);

            if (foundUserSalary == null)
                return NotFound();

            return Ok(foundUserSalary);
        }

        [HttpDelete("UserSalary/{userId}")]
        public IActionResult DeleteUserSalary([FromRoute] int userId)
        {
            UserSalary? foundUserSalary = _userRepository.DeleteUserSalary(userId);

            if (foundUserSalary == null)
                return NotFound();

            return NoContent();
        }
    }
}