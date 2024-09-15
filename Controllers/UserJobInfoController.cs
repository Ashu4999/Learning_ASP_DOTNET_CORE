using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning_Dotnet.Data;
using Learning_Dotnet.Dtos;
using Learning_Dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserJobInfoController : ControllerBase
    {
        private readonly DataContextDapper _dataContextDapper;
        public UserJobInfoController(IConfiguration config)
        {
            _dataContextDapper = new DataContextDapper(config);
        }

        [HttpGet]
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

        [HttpGet("{userId}")]
        public IActionResult GetUserJobInfo([FromRoute] int userId)
        {
            string sql = $"SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId}";
            UserJobInfo foundUserJobInfo = _dataContextDapper.LoadDataSingle<UserJobInfo>(sql);
            return Ok(foundUserJobInfo);
        }

        [HttpPost]
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

        [HttpPut]
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


        [HttpDelete("{userId}")]
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
    }
}