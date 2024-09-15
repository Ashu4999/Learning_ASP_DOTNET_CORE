using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning_Dotnet.Data;
using Learning_Dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserSalaryEFController : ControllerBase
    {
        private readonly DataContextEF _dataContextEF;
        public UserSalaryEFController(IConfiguration config)
        {
            _dataContextEF = new DataContextEF(config);
        }

        [HttpGet]
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

        [HttpGet("{userId}")]
        public IActionResult GetUserSalary([FromRoute] int userId)
        {
            UserSalary? foundUserSalary = _dataContextEF.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserSalary == null)
                return NotFound();

            return Ok(foundUserSalary);
        }

        [HttpPost]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            _dataContextEF.UserSalary.Add(userSalary);
            _dataContextEF.SaveChanges();
            return Ok(userSalary);
        }

        [HttpPut]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            UserSalary? foundUserSalary = _dataContextEF.UserSalary.Where(u => u.UserId == userSalary.UserId).FirstOrDefault();

            if (foundUserSalary == null)
                return NotFound();

            foundUserSalary.Salary = userSalary.Salary;

            _dataContextEF.SaveChanges();
            return Ok(foundUserSalary);
        }

        [HttpDelete("{userId}")]
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