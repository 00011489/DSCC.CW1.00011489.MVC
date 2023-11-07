using EmployeeManagement_Front.Models;
using System.Net;

namespace EmployeeManagement_Front.Services
{
    public class EmployeeService : IEmployeeService
    {

        private static List<Employee> _employees = new List<Employee>();
        private readonly HttpClient _httpClient;
        private readonly string baseUri;

        // Constructor for EmployeeService
        public EmployeeService(HttpClient httpClient, IConfiguration configuration)
        {
            // Initialize the HttpClient and baseUri
            _httpClient = httpClient;
            baseUri = configuration["BaseUri"];
        }

        // Adds an employee using a DTO (Data Transfer Object)
        public async Task<Boolean> AddEmployee(EmployeeDto dto)
        {
            // Send a POST request to the specified URI to add the employee
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<EmployeeDto>(baseUri + "Employee", dto);

            // Since we're not checking the response, return a fixed 'true'
            return true;
        }

        // Deletes an employee by ID
        public async Task<Boolean> DeleteEmployee(int id)
        {
            // Send a DELETE request to the specified URI to delete the employee
            Employee employee = await _httpClient.DeleteFromJsonAsync<Employee>(baseUri + $"Employee/{id}");

            // Check if the employee was successfully deleted and return a boolean result
            if (employee != null)
            {
                return true;
            }
            return false;
        }

        // Gets a list of all employees asynchronously
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            // Build the URI to retrieve all employees
            string uri = baseUri + "Employee";

            // Send a GET request to the specified URI to get all employees
            List<Employee> result = await _httpClient.GetFromJsonAsync<List<Employee>>(uri);

            // Return the list of employees
            return result;
        }

        // Gets an employee by ID asynchronously
        public async Task<Employee> GetEmployeeById(int id)
        {
            // Build the URI to retrieve the employee by ID
            var employee = await _httpClient.GetFromJsonAsync<Employee>(baseUri + $"Employee/{id}");

            // Return the employee
            return employee;
        }

        // Updates an existing employee
        public async Task<Boolean> UpdateEmployee(Employee employee)
        {
            // Retrieve the existing employee by ID
            var existingEmployee = await _httpClient.GetFromJsonAsync<Employee>(baseUri + $"Employee/{employee.Id}");

            // If the existing employee is found, update its properties
            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.Age = employee.Age;

                // Send a PUT request to update the employee details
                await _httpClient.PutAsJsonAsync<Employee>(baseUri + $"Employee/{employee.Id}", existingEmployee);

                // Return 'true' to indicate successful update
                return true;
            }

            // Return 'false' to indicate update failure
            return false;
        }

    }
}
