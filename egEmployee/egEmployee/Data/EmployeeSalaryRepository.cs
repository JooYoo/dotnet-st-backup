using egEmployee.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace egEmployee.Data
{
    public class EmployeeSalaryRepository
    {
        // dependency injection: include connectionString to connect with DB
        private readonly string _connectionString;
        public EmployeeSalaryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        // async task: connect with SQL Procedure; convert data to List<T>
        public async Task<List<EmployeeSalary>> GetSalaries()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllSalary", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<EmployeeSalary>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }


        private EmployeeSalary MapToValue(SqlDataReader reader)
        {
            return new EmployeeSalary()
            {
                EmployeeID = (int)reader["EmployeeID"],
                JobTitle = reader["JobTitle"].ToString(),
                Salary = (int)reader["Salary"]
            };
        }
    }
}
