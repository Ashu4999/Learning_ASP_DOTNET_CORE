using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Learning_Dotnet.Data;
using Learning_Dotnet.Dtos;
using Learning_Dotnet.Interfaces;
using Learning_Dotnet.Models;

namespace Learning_Dotnet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContextEF _dataContextEF;
        IMapper _mapper;

        public UserRepository(IConfiguration config)
        {
            _dataContextEF = new DataContextEF(config);

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserAddDto, User>();
                cfg.CreateMap<User, User>();
                cfg.CreateMap<UserJobInfo, UserJobInfo>();
                cfg.CreateMap<UserSalary, UserSalary>();
            }));
        }

        public IEnumerable<User> GetUsers(int pageNo, int pageSize)
        {
            IQueryable<User> userQuery = _dataContextEF.Users;

            if (pageNo > 0 && pageSize > 0)
            {
                int skip = (pageNo - 1) * pageSize;
                userQuery = userQuery.Skip(skip).Take(pageSize);
            }

            IEnumerable<User> users = userQuery.ToList();
            return users;
        }

        public User? GetUser(int userId)
        {
            return _dataContextEF.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public User? GetUserByEmail(string email)
        {
            return _dataContextEF.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User CreateUser(UserAddDto user)
        {
            User newUser = _mapper.Map<User>(user);
            _dataContextEF.Users.Add(newUser);
            _dataContextEF.SaveChanges();
            return newUser;
        }

        public User? UpdateUser(int userId, User user)
        {
            User? foundUser = GetUser(userId);

            if (foundUser == null)
                return null;

            _mapper.Map(user, foundUser);
            _dataContextEF.SaveChanges();
            return foundUser;
        }

        public User? DeleteUser(int userId)
        {
            User? foundUser = GetUser(userId);

            if (foundUser == null)
                return null;

            _dataContextEF.Users.Remove(foundUser);
            _dataContextEF.SaveChanges();
            return foundUser;
        }

        public IEnumerable<UserJobInfo> GetUserJobInfos(int pageNo, int pageSize)
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

        public UserJobInfo? GetUserJobInfo(int userId)
        {
            return _dataContextEF.UserJobInfo.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public UserJobInfo CreateUserJobInfo(UserJobInfo userJobInfo)
        {
            _dataContextEF.UserJobInfo.Add(userJobInfo);
            _dataContextEF.SaveChanges();
            return userJobInfo;
        }

        public UserJobInfo? UpdateUserJobInfo(int userId, UserJobInfo userJobInfo)
        {
            UserJobInfo? foundUserJobInfo = GetUserJobInfo(userId);

            if (foundUserJobInfo == null)
                return null;

            _mapper.Map(userJobInfo, foundUserJobInfo);
            _dataContextEF.SaveChanges();
            return foundUserJobInfo;
        }

        public UserJobInfo? DeleteUserJobInfo(int userId)
        {
            UserJobInfo? foundUserJobInfo = GetUserJobInfo(userId);

            if (foundUserJobInfo == null)
                return null;

            _dataContextEF.UserJobInfo.Remove(foundUserJobInfo);
            _dataContextEF.SaveChanges();
            return foundUserJobInfo;
        }

        public IEnumerable<UserSalary> GetUsersSalaries(int pageNo, int pageSize)
        {
            Console.WriteLine($"{pageNo} {pageSize}");
            IQueryable<UserSalary> userSalaryQuery = _dataContextEF.UserSalary;

            // if (pageNo > 0 && pageSize > 0)
            // {
            //     int skip = (pageNo - 1) * pageSize;
            //     userSalaryQuery = userSalaryQuery.Skip(skip).Take(pageSize);
            // }
            IEnumerable<UserSalary> userSalaries = userSalaryQuery.ToList();
            return userSalaries;
        }

        public UserSalary? GetUserSalary(int userId)
        {
            return _dataContextEF.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public UserSalary CreateUserSalary(UserSalary userSalary)
        {
            _dataContextEF.UserSalary.Add(userSalary);
            _dataContextEF.SaveChanges();
            return userSalary;
        }

        public UserSalary? UpdateUserSalary(int userId, UserSalary userSalary)
        {
            UserSalary? foundUserSalary = GetUserSalary(userId);

            if (foundUserSalary == null)
                return null;

            _mapper.Map(userSalary, foundUserSalary);
            _dataContextEF.SaveChanges();
            return foundUserSalary;
        }

        public UserSalary? DeleteUserSalary(int userId)
        {
            UserSalary? foundUserSalary = _dataContextEF.UserSalary.Where(u => u.UserId == userId).FirstOrDefault();

            if (foundUserSalary == null)
                return null;

            _dataContextEF.UserSalary.Remove(foundUserSalary);
            _dataContextEF.SaveChanges();
            return foundUserSalary;
        }
    }
}