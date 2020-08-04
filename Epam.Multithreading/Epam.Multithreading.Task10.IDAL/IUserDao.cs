using Epam.Multithreading.Task10.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Multithreading.Task10.IDAL
{
    public interface IUserDao
    {
        int CreateUser(User user);
        bool UpdateUser(User user);
        IEnumerable<User> GetUsers();
        User GetUser(int Id);
        bool DeleteUser(int Id);
    }
}
