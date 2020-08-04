using Epam.Multithreading.Task10.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Multithreading.Task10.IBLL
{
    public interface IUserLogic
    {
        int CreateUser(User user);
        bool UpdateUser(User user);
        IEnumerable<User> GetUsers();
        User GetUser(int Id);
        bool DeleteUser(int Id);
    }
}
