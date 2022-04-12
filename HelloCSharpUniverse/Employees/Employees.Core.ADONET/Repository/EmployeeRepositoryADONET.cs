using Employees.Core.Entities;
using Employees.Core.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Employees.Core.ADONET.Repository
{
    public class EmployeeRepositoryADONET : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepositoryADONET()
        {
            // Read configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve connection string from configuration
            string cs = config.GetConnectionString("employees");
        }

        public bool Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(object id)
        {
            if (id == null)
                return false;

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM employy WHERE id = @id";

                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = id
                    });

                    int result = cmd.ExecuteNonQuery();

                    if (result != 1)
                        return false;

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    if (connection.State != System.Data.ConnectionState.Closed)
                        connection.Close();
                }
            }
        }

        public IEnumerable<Employee> Fetch(Func<Employee, bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public Employee GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Employee GetById(object id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT TOP(1) id, code, first_name, last_name FROM employee WHERE @id = id";
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = id
                });

                var reader = cmd.ExecuteReader();

                if (!reader.Read())
                    return null;

                Employee emp = new Employee
                {
                    Id = reader.GetInt32(0),
                    Code = reader.GetString(1),
                    FirstName = reader.GetString(2),
                    LastName = reader.GetString(3),
                    AnnualSalary = reader.GetDecimal(4),
                };

                try
                {
                    return emp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
