using Epam.Multithreading.Task10.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task10.IDAL
{
    public interface IUserDao
    {
        Task<int> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<IEnumerable<User>>  GetUsers();
        Task<User> GetUser(int Id);
        Task<bool> DeleteUser(int Id);
    }
}
