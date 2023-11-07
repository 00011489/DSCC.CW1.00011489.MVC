using EmployeeManagement_Front.Models;
using EmployeeManagement_Front.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Front.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: /Employee/Index
        // This action returns a view named "Index" which will be rendered in the browser.
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Employee/GetEmployees
        // This action retrieves a list of employees as JSON and returns it to the client.
        // It's marked as asynchronous because it involves an asynchronous operation (database call).
        // The 'await' keyword is used to await the result of the asynchronous operation.
        public async Task<JsonResult> GetEmployees()
        {
            // Asynchronously call the employeeService to get all employees.
            List<Employee> employees = await employeeService.GetAllEmployeesAsync();

            // Return the list of employees as JSON to the client.
            return Json(employees);
        }

        // POST: /Employee/Insert
        // This action inserts a new employee.
        // It's marked as asynchronous because it involves an asynchronous operation (database call).
        // The 'await' keyword is used to await the result of the asynchronous operation.
        [HttpPost]
        public async Task<JsonResult> Insert(EmployeeDto createDto)
        {
            // Check if the data provided in createDto passes model validation.
            if (ModelState.IsValid)
            {
                // Asynchronously call the employeeService to add a new employee.
                await employeeService.AddEmployee(createDto);

                // Return a success message as JSON to the client.
                return Json("Employee details saved.");
            }
            else
            {
                // If model validation fails, return an error message as JSON to the client.
                return Json("Model validation failed");
            }
        }

        // GET: /Employee/Edit/{id}
        // This action retrieves an employee by ID as JSON for editing.
        // It's marked as asynchronous because it involves an asynchronous operation (database call).
        // The 'await' keyword is used to await the result of the asynchronous operation.
        [HttpGet]
        public async Task<JsonResult> Edit(int id)
        {
            // Asynchronously call the employeeService to get an employee by ID.
            var employee = await employeeService.GetEmployeeById(id);

            // Return the employee as JSON to the client.
            return Json(employee);
        }

        // PUT: /Employee/Update
        // This action updates an employee.
        // It's marked as asynchronous because it involves an asynchronous operation (database call).
        // The 'await' keyword is used to await the result of the asynchronous operation.
        [HttpPut]
        public async Task<JsonResult> Update(Employee employee)
        {
            // Check if the data provided in the 'employee' parameter passes model validation.
            if (ModelState.IsValid)
            {
                // Asynchronously call the employeeService to update an employee.
                var result = await employeeService.UpdateEmployee(employee);

                // Return a success message as JSON to the client.
                return Json("Product details updated");
            }

            // If model validation fails, return an error message as JSON to the client.
            return Json("Model validation failed");
        }

        // DELETE: /Employee/Delete/{id}
        // This action deletes an employee by ID.
        public async Task<JsonResult> Delete(int id)
        {
            // Asynchronously call the employeeService to delete an employee by ID.
            employeeService.DeleteEmployee(id);

            // Return a success message as JSON to the client.
            return Json("Employee Deleted Successfully!");
        }
    }
}
