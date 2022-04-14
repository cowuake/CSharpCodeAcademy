using Employees.Core.Entities;
using Employees.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employees.WebAPI.Controllers
{
    // Match the controller
    [Route("api/[controller]")] // To redirect to this controller, [...]/api/... as address
    [ApiController] // Not strictly needed unless we want to delegate actions to the controller
    public class EmployeeController : ControllerBase
    {
        private readonly IMainBusinessLogic _logic;

        public EmployeeController(IMainBusinessLogic logic) // Constructor injection
        {
            _logic = logic;
        }

        [HttpGet] // REST requires the action to be specified by the method
        public IActionResult GetAllEmployees()
        {
            var result = _logic.GetAllEmployees();

            // View or HTTP request response (here we don't have views)
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (employee == null)
                return BadRequest("Invalid employee data.");

            bool result = _logic.AddNewEmployee(employee);

            if (!result)
                return StatusCode(500, "Cannot save employee.");

            return NoContent(); // 204?

            //return Ok(result);
        }

        [HttpGet("{id}")] // api/employees/N
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        public IActionResult GetEmployy(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid employee id."); // Error 400

            var result = _logic.GetEmployeeById(id);

            if (result == null)
                return NotFound("Employee not found."); // Error 404

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid employee id."); // Error 400

            var result = _logic.DeleteEmployeeById(id);

            if (!result)
                return StatusCode(500, "Cannot delete employee.");

            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            if (id <= 0 || employee == null)
                return BadRequest("Invalid parameters.");

            if (id != employee.Id)
                return BadRequest("Employee's ID does not match.");

            var result = _logic.UpdateEmployee(employee);

            if (!result)
                return StatusCode(500, "Cannot update employee data.");

            return Ok(employee);
        }
    }
}