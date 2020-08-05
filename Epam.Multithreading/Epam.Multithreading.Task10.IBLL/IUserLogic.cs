using Epam.Multithreading.Task10.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task10.IBLL
{
    public interface IUserLogic
    {
        Task<int> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int Id);
        Task<bool> DeleteUser(int Id);
    }
}
