using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public Task<int> CreateUser(User user)
        {
            return _userDao.CreateUser(user);
        }

        public Task<bool> DeleteUser(int Id)
        {
            return _userDao.DeleteUser(Id);
        }

        public Task<User> GetUser(int Id)
        {
            return _userDao.GetUser(Id);
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            return _userDao.GetUsers();
        }

        public Task<bool> UpdateUser(User user)
        {
            return _userDao.UpdateUser(user);
        }
    }
}
