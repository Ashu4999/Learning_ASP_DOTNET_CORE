using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning_Dotnet.Data;
using Learning_Dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserSalaryController : ControllerBase
    {
        private readonly DataContextDapper _dataContextDapper;
        public UserSalaryController(IConfiguration config)
        {
            _dataContextDapper = new DataContextDapper(config);
        }

        [HttpGet]
        public IEnumerable<UserSalary> GetUserJobInfo([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            string sql = "SELECT * FROM TutorialAppSchema.UserSalary";

            if (pageNo > 0 && pageSize > 0)
            {
                int skip = (pageNo - 1) * pageSize;
                sql += $" ORDER BY UserId OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY;";
            }
            IEnumerable<UserSalary> userSalaries = _dataContextDapper.LoadData<UserSalary>(sql);
            return userSalaries;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserSalary([FromRoute] int userId)
        {
            string sql = $"SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = {userId}";
            UserJobInfo foundUserSalary = _dataContextDapper.LoadDataSingle<UserJobInfo>(sql);
            return Ok(foundUserSalary);
        }

        [HttpPost]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            string sql = @$"
            INSERT INTO TutorialAppSchema.UserSalary(
	            [UserId], [Salary]
            ) VALUES
                ('{userSalary.UserId}', '{userSalary.Salary}'
            );";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to add userSalary");
        }

        [HttpPut]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            string sql = @$"
            UPDATE TutorialAppSchema.UserSalary
                SET
                    UserId = {userSalary.UserId},
                    Salary = '{userSalary.Salary}'
                WHERE 
                    UserId = {userSalary.UserId};
            ";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to edit userSalary");
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUserSalary([FromRoute] int userId)
        {
            string sql = $"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = {userId}";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to delete userSalary");
        }
    }
}