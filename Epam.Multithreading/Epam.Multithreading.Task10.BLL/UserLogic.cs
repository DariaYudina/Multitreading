using System;
using System.Collections.Generic;
using System.Text;
using Epam.Multithreading.Task10.Entities;
using Epam.Multithreading.Task10.IBLL;
using Epam.Multithreading.Task10.IDAL;

namespace Epam.Multithreading.Task10.BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;

        public UserLogic(IUserDao userDao)
        {
            _userDao = userDao;
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
