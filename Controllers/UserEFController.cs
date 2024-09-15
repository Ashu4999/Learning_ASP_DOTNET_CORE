using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learning_Dotnet.Data;
using Learning_Dotnet.Dtos;
using Learning_Dotnet.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserEFController : ControllerBase
    {
        private readonly DataContextEF _dataContextEF;
        IMapper _mapper;
        public UserEFController(IConfiguration config)
        {
            _dataContextEF = new DataContextEF(config);
            _mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<UserAddDto, User>();
                cfg.CreateMap<User, User>();
                cfg.CreateMap<UserJobInfo, UserJobInfo>();
                cfg.CreateMap<UserSalary, UserSalary>();
            }));
        }

        [HttpGet]
        public IEnumerable<User> GetUsers([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            IQueryable<User> userQuery = _dataContextEF.Users;

            if (pageNo != null && pageSize != null && pageNo > 0 && pageSize > 0)
            {
                int skip = (pageNo - 1) * pageSize;
                userQuery = userQuery.Skip(skip).Take(pageSize);
            }

            IEnumerable<User> users = userQuery.ToList();
            return users;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser([FromRoute] int userId)
        {
            User? foundUser = _dataContextEF.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUser == null)
                return NotFound();

            return Ok(foundUser);
        }

        [HttpPost]
        public IActionResult AddUser(UserAddDto user)
        {
            User newUser = _mapper.Map<User>(user);

            _dataContextEF.Users.Add(newUser);
            _dataContextEF.SaveChanges();
            return Ok(newUser);
        }

        [HttpPut]
        public IActionResult EditUser(User user)
        {
            User? foundUser = _dataContextEF.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            if (foundUser == null)
                return NotFound();

            _mapper.Map(user, foundUser);

            _dataContextEF.SaveChanges();
            return Ok(foundUser);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser([FromRoute] int userId)
        {
            User? foundUser = _dataContextEF.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUser == null)
                return NotFound();

            _dataContextEF.Users.Remove(foundUser);
            _dataContextEF.SaveChanges();
            return NoContent();
        }

        [HttpGet("UserJobInfo")]
        public IEnumerable<UserJobInfo> GetUserJobInfo([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            IQueryable<UserJobInfo> userJobInfoQuery = _dataContextEF.UserJobInfo;

            if (pageNo > 0 && pageSize > 0)
            {
                int skip = (pageNo - 1) * pageSize;
                userJobInfoQuery = userJobInfoQuery.Skip(skip).Take(pageSize);
            }
            IEnumerable<UserJobInfo> userJobInfos = userJobInfoQuery.ToList();
            return userJobInfos;
        }

        [HttpGet("UserJobInfo/{userId}")]
        public IActionResult GetUserJobInfo([FromRoute] int userId)
        {
            UserJobInfo? foundUserJobInfo = _dataContextEF.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserJobInfo == null)
                return NotFound();

            return Ok(foundUserJobInfo);
        }

        [HttpPost("UserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            _dataContextEF.UserJobInfo.Add(userJobInfo);
            _dataContextEF.SaveChanges();
            return Ok(userJobInfo);
        }

        [HttpPut("UserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            UserJobInfo? foundUserJobInfo = _dataContextEF.UserJobInfo.Where(u => u.UserId == userJobInfo.UserId).FirstOrDefault();

            if (foundUserJobInfo == null)
                return NotFound();

            // foundUserJobInfo.JobTitle = userJobInfo.JobTitle;
            // foundUserJobInfo.Department = userJobInfo.Department;
            _mapper.Map(userJobInfo, foundUserJobInfo);
            _dataContextEF.SaveChanges();
            return Ok(foundUserJobInfo);
        }


        [HttpDelete("UserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo([FromRoute] int userId)
        {
            UserJobInfo? foundUserJobInfo = _dataContextEF.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserJobInfo == null)
                return NotFound();

            _dataContextEF.UserJobInfo.Remove(foundUserJobInfo);
            _dataContextEF.SaveChanges();
            return NoContent();
        }

        [HttpGet("UserSalary")]
        public IEnumerable<UserSalary> GetUserSalaries([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            IQueryable<UserSalary> userSalaryQuery = _dataContextEF.UserSalary;

            if (pageNo > 0 && pageSize > 0)
            {
                int skip = (pageNo - 1) * pageSize;
                userSalaryQuery = userSalaryQuery.Skip(skip).Take(pageSize);
            }
            IEnumerable<UserSalary> userSalaries = userSalaryQuery.ToList();
            return userSalaries;
        }

        [HttpGet("UserSalary/{userId}")]
        public IActionResult GetUserSalary([FromRoute] int userId)
        {
            UserSalary? foundUserSalary = _dataContextEF.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserSalary == null)
                return NotFound();

            return Ok(foundUserSalary);
        }

        [HttpPost("UserSalary")]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            _dataContextEF.UserSalary.Add(userSalary);
            _dataContextEF.SaveChanges();
            return Ok(userSalary);
        }

        [HttpPut("UserSalary")]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            UserSalary? foundUserSalary = _dataContextEF.UserSalary.Where(u => u.UserId == userSalary.UserId).FirstOrDefault();

            if (foundUserSalary == null)
                return NotFound();

            // foundUserSalary.Salary = userSalary.Salary;
            _mapper.Map(userSalary, foundUserSalary);

            _dataContextEF.SaveChanges();
            return Ok(foundUserSalary);
        }

        [HttpDelete("UserSalary/{userId}")]
        public IActionResult DeleteUserSalary([FromRoute] int userId)
        {
            UserSalary? foundUserSalary = _dataContextEF.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserSalary == null)
                return NotFound();

            _dataContextEF.UserSalary.Remove(foundUserSalary);
            _dataContextEF.SaveChanges();
            return NoContent();
        }
    }
}