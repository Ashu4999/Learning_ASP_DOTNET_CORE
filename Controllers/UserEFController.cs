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

            _dataContextEF.Add(newUser);
            _dataContextEF.SaveChanges();
            return Ok(newUser);
        }

        [HttpPut]
        public IActionResult EditUser(User user)
        {
            User? foundUser = _dataContextEF.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();

            if (foundUser == null)
                return NotFound();

            foundUser.FirstName = user.FirstName;
            foundUser.LastName = user.LastName;
            foundUser.Email = user.Email;
            foundUser.Gender = user.Gender;
            foundUser.Active = user.Active;

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
    }
}