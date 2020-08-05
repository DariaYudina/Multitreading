using Epam.Multithreading.Task10.Entities;
using Epam.Multithreading.Task10.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task10.DAL
{
    public class UserDao : IUserDao
    {
        private readonly string _connectionString;

        public UserDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "CreateUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter FirstName = new SqlParameter
                    {
                        ParameterName = "@FirstName",
                        Value = user.FirstName,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter LastName = new SqlParameter
                    {
                        ParameterName = "@LastName",
                        Value = user.LastName,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter Age = new SqlParameter
                    {
                        ParameterName = "@Age",
                        Value = user.Age,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(FirstName);
                    command.Parameters.Add(LastName);
                    command.Parameters.Add(Age);
                    await connection.OpenAsync();
                    object id = await command.ExecuteScalarAsync();
                    if(id != null || id != DBNull.Value)
                    {
                        return (int)id;
                    }

                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public async Task<bool> DeleteUser(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "DeleteUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter UserId = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = Id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(UserId);
                    await connection.OpenAsync();
                    int count = await command.ExecuteNonQueryAsync();
                    return count > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<User> GetUser(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "GetUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter UserId = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = Id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(UserId);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            return new User()
                            {
                                Id = (int)(reader["Id"]),
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Age = (int)(reader["Age"])
                            };
                        }
                    }
                    reader.Close();
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "GetUsers";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;

                SqlDataReader reader = await command.ExecuteReaderAsync();
                return ReadItems(reader).ToArray();
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "UpdateUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter UserId = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = user.Id,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter FirstName = new SqlParameter
                    {
                        ParameterName = "@FirstName",
                        Value = user.FirstName,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter LastName = new SqlParameter
                    {
                        ParameterName = "@LastName",
                        Value = user.LastName,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };

                    SqlParameter Age = new SqlParameter
                    {
                        ParameterName = "@Age",
                        Value = user.Age,
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input
                    };

                    command.Parameters.Add(UserId);
                    command.Parameters.Add(FirstName);
                    command.Parameters.Add(LastName);
                    command.Parameters.Add(Age);
                    await connection.OpenAsync();
                    int count = await command.ExecuteNonQueryAsync();
                    return count > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private IEnumerable<User> ReadItems(SqlDataReader reader)
        {
            while (reader.Read())
            {
                yield return new User()
                {
                    Id = (int)(reader["Id"]),
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)(reader["Age"])
                };
            }
        }
    }
}
