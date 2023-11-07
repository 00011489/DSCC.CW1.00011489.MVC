using EmployeeManagement_Front.Models;

namespace EmployeeManagement_Front.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeById(int id);
        Task<Boolean> AddEmployee(EmployeeDto dto);
        Task<Boolean> UpdateEmployee(Employee employee);
        Task<Boolean> DeleteEmployee(int id);
    }
}
