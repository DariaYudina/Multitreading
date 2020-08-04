using Epam.Multithreading.Task10.Entities;
using Epam.Multithreading.Task10.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Epam.Multithreading.Task10.DAL
{
    public class UserDao : IUserDao
    {
        private readonly string _connectionString;

        public UserDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
