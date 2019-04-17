using Capstone.Models;
using Capstone.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAL.Interfaces
{
    public interface IUserSqlDal
    {

        bool CreateUser(User user);
        void DeleteUser(User user);
        User GetUser(string email);
        void UpdateUser(User user);
        bool DuplicateUser(User user);
        List<User> GetAllUsers();
        List<User> GetAllUsersByRole();
        bool UpdateUserRole(UserRoleAdminViewModel model);
        User GetUserByID(int id);
    }
}
