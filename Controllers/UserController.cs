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
    }
}