using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning_Dotnet.Dtos;
using Learning_Dotnet.Models;

namespace Learning_Dotnet.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(int pageNo, int pageSize);
        User? GetUser(int userId);
        User? GetUserByEmail(string email);
        User CreateUser(UserAddDto user);
        User? UpdateUser(int userId, User user);
        User? DeleteUser(int userId);
        IEnumerable<UserJobInfo> GetUserJobInfos(int pageNo, int pageSize);
        UserJobInfo? GetUserJobInfo(int userId);
        UserJobInfo CreateUserJobInfo(UserJobInfo userJobInfo);
        UserJobInfo? UpdateUserJobInfo(int userId, UserJobInfo userJobInfo);
        UserJobInfo? DeleteUserJobInfo(int userId);
        IEnumerable<UserSalary> GetUsersSalaries(int pageNo, int pageSize);
        UserSalary? GetUserSalary(int userId);
        UserSalary CreateUserSalary(UserSalary userSalary);
        UserSalary? UpdateUserSalary(int userId, UserSalary userSalary);
        UserSalary? DeleteUserSalary(int userId);
    }
}