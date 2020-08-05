using Epam.Multithreading.Task10.BLL;
using Epam.Multithreading.Task10.DAL;
using Epam.Multithreading.Task10.IBLL;
using Epam.Multithreading.Task10.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Epam.Multithreading.Task10.Common
{
    public static class DependencyResolver
    {
        public static IUserLogic UserLogic { get; }

        public static IUserDao UserDao { get; }

        static DependencyResolver()
        {
            string sqlConnection = ConfigurationManager.ConnectionStrings["UsersDB"].ConnectionString;
            UserDao = new UserDao(sqlConnection);
            UserLogic = new UserLogic(UserDao);
        }
    }
}
