using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using RepositoryDesignPattern.Models;
using Microsoft.Extensions.Configuration;

namespace RepositoryDesignPattern.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task AddEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("insert into tblemployee (empname,designation,salary) values (@name,@designation,'"+employee.salary+"')",conn);
            cmd.Parameters.AddWithValue("@name",employee.Empname);
            cmd.Parameters.AddWithValue("@designation",employee.Designation);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteEmployee(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("delete from tblemployee where empid=@id",conn);
            cmd.Parameters.AddWithValue("@id",id);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Employee>> GetAllEmployee()
          {
            var employee = new List<Employee>();
            SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("select empid,empname,designation,salary from tblemployee",conn);
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employee.Add(new Employee
                {
                    Empid = (int)reader["empid"],
                    Empname = (string)reader["empname"],
                    Designation = (string)reader["designation"],
                    salary = (decimal)reader["salary"]
                });
            }
            return employee;
        }

        public async Task<Employee> GetEmployeeByID(int id)
        {
            Employee emp = new Employee();
            SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("select empid,empname,designation,salary from tblemployee where empid='"+id+"'",conn);
            SqlDataReader reader= await cmd.ExecuteReaderAsync();
            if(await reader.ReadAsync())
            {

                emp.Empid = (int)reader["empid"];
                emp.Empname = (string)reader["empname"];
                emp.Designation = (string)reader["designation"];
                emp.salary = (decimal)reader["salary"];
               
            }
            return emp;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("update tblemployee set empname=@name,designation=@designation,salary=@salary where empid=@empid",conn);
            cmd.Parameters.AddWithValue("@name",employee.Empname);
            cmd.Parameters.AddWithValue("@designation", employee.Designation);
            cmd.Parameters.AddWithValue("@salary", employee.salary);
            cmd.Parameters.AddWithValue("@empid",employee.Empid);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
