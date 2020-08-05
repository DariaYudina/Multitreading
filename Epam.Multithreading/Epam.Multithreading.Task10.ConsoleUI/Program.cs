using Epam.Multithreading.Task10.Common;
using Epam.Multithreading.Task10.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task10.ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int id = await DependencyResolver.UserLogic.CreateUser(new User() { FirstName ="Test" , LastName = "Test", Age = 20});
            bool deleted = await DependencyResolver.UserLogic.DeleteUser(1);
            User user = await DependencyResolver.UserLogic.GetUser(1);
            IEnumerable<User> users = await DependencyResolver.UserLogic.GetUsers();
            bool updated = await DependencyResolver.UserLogic.UpdateUser(new User() { Id = 4, FirstName = "Update", LastName = "Update", Age = 1});
        }
    }
}
