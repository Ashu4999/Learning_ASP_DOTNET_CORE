using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Learning_Dotnet.Data;
using Learning_Dotnet.Dtos;
using Learning_Dotnet.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContextDapper _dataContextDapper;
        public UserController(IConfiguration config)
        {
            _dataContextDapper = new DataContextDapper(config);
        }

        [HttpGet]
        public IEnumerable<User> GetUsers([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            string sql = "SELECT * FROM TutorialAppSchema.Users";

            if (pageNo > 0 && pageSize > 0) {
                int skip = (pageNo - 1) * pageSize;
                sql += $" ORDER BY UserId OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY;";
            }
            
            IEnumerable<User> users = _dataContextDapper.LoadData<User>(sql);
            return users;
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser([FromRoute] int userId)
        {
            string sql = $"SELECT * FROM TutorialAppSchema.Users WHERE UserId = {userId}";
            User foundUser = _dataContextDapper.LoadDataSingle<User>(sql);
            return Ok(foundUser);
        }

        [HttpPost]
        public IActionResult AddUser(UserAddDto user)
        {
            string sql = @$"
            INSERT INTO TutorialAppSchema.Users(
	            [FirstName], [LastName], [Email], [Gender], [Active]
            ) VALUES
                ('{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Gender}', '{user.Active}'
            );";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to add user");
        }

        [HttpPut]
        public IActionResult EditUser(User user)
        {
            string sql = @$"
            UPDATE TutorialAppSchema.Users
                SET
                    FirstName = '{user.FirstName}', 
                    LastName = '{user.LastName}', 
                    Email = '{user.Email}', 
                    Gender = '{user.Gender}', 
                    Active = '{user.Active}'
                WHERE 
                    UserId = {user.UserId};
            ";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to edit user");
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser([FromRoute] int userId)
        {
            string sql = $"DELETE FROM TutorialAppSchema.Users WHERE UserId = {userId}";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to delete user");
        }

        [HttpGet("UserJobInfo")]
        public IEnumerable<UserJobInfo> GetUserJobInfo([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            string sql = "SELECT * FROM TutorialAppSchema.UserJobInfo";

            if (pageNo > 0 && pageSize > 0)
            {
                int skip = (pageNo - 1) * pageSize;
                sql += $" ORDER BY UserId OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY;";
            }
            IEnumerable<UserJobInfo> userJobInfos = _dataContextDapper.LoadData<UserJobInfo>(sql);
            return userJobInfos;
        }

        [HttpGet("UserJobInfo/{userId}")]
        public IActionResult GetUserJobInfo([FromRoute] int userId)
        {
            string sql = $"SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId}";
            UserJobInfo foundUserJobInfo = _dataContextDapper.LoadDataSingle<UserJobInfo>(sql);
            return Ok(foundUserJobInfo);
        }

        [HttpPost("UserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            string sql = @$"
            INSERT INTO TutorialAppSchema.UserJobInfo(
	            [UserId], [JobTitle], [Department]
            ) VALUES
                ('{userJobInfo.UserId}', '{userJobInfo.JobTitle}', '{userJobInfo.Department}'
            );";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to add userJobInfo");
        }

        [HttpPut("UserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            string sql = @$"
            UPDATE TutorialAppSchema.UserJobInfo
                SET
                    UserId = {userJobInfo.UserId},
                    JobTitle = '{userJobInfo.JobTitle}', 
                    Department = '{userJobInfo.Department}'
                WHERE 
                    UserId = {userJobInfo.UserId};
            ";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to edit userJobInfo");
        }

        [HttpDelete("UserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo([FromRoute] int userId)
        {
            string sql = $"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId}";
            bool result = _dataContextDapper.ExecuteSql(sql);
            if (result)
            {
                return Ok();
            }
            throw new Exception("Failed to delete userJobInfo");
        }

        [HttpGet("UserSalary")]
        public IEnumerable<UserSalary> GetUserSalary([FromQuery] int pageNo, [FromQuery] int pageSize)
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

        [HttpGet("UserSalary/{userId}")]
        public IActionResult GetUserSalary([FromRoute] int userId)
        {
            string sql = $"SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = {userId}";
            UserJobInfo foundUserSalary = _dataContextDapper.LoadDataSingle<UserJobInfo>(sql);
            return Ok(foundUserSalary);
        }

        [HttpPost("UserSalary")]
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

        [HttpPut("UserSalary")]
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

        [HttpDelete("UserSalary/{userId}")]
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