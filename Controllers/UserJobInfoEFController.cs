using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learning_Dotnet.Data;
using Learning_Dotnet.Dtos;
using Learning_Dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserJobInfoEFController : ControllerBase
    {
        private readonly DataContextEF _dataContextEF;
        // private Mapper _mapper;
        public UserJobInfoEFController(IConfiguration config)
        {
            _dataContextEF = new DataContextEF(config);
            // _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserJobInfo, UserJobInfo>()));
        }

        [HttpGet]
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

        [HttpGet("{userId}")]
        public IActionResult GetUserJobInfo([FromRoute] int userId)
        {
            UserJobInfo? foundUserJobInfo = _dataContextEF.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserJobInfo == null)
                return NotFound();

            return Ok(foundUserJobInfo);
        }

        [HttpPost]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            _dataContextEF.UserJobInfo.Add(userJobInfo);
            _dataContextEF.SaveChanges();
            return Ok(userJobInfo);
        }

        [HttpPut]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            UserJobInfo? foundUserJobInfo = _dataContextEF.UserJobInfo.Where(u => u.UserId == userJobInfo.UserId).FirstOrDefault();

            if (foundUserJobInfo == null)
                return NotFound();

            foundUserJobInfo.JobTitle = userJobInfo.JobTitle;
            foundUserJobInfo.Department = userJobInfo.Department;
            _dataContextEF.SaveChanges();
            return Ok(foundUserJobInfo);
        }


        [HttpDelete("{userId}")]
        public IActionResult DeleteUserJobInfo([FromRoute] int userId)
        {
            UserJobInfo? foundUserJobInfo = _dataContextEF.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserJobInfo == null)
                return NotFound();

            _dataContextEF.UserJobInfo.Remove(foundUserJobInfo);
            _dataContextEF.SaveChanges();
            return NoContent();
        }
    }
}