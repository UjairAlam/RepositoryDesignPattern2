using RepositoryDesignPattern.Models;

namespace RepositoryDesignPattern.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployee();
        Task<Employee> GetEmployeeByID(int id);
        Task AddEmployee(Employee employee);
        Task DeleteEmployee(int id);
        Task UpdateEmployee(Employee employee);
    }
}
