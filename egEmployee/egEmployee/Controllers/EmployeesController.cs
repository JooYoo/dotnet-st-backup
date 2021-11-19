using egEmployee.Data;
using egEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace egEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        // dependency injection: include EmployeeSalaryRepository
        private readonly EmployeeSalaryRepository _repository;
        public EmployeesController(EmployeeSalaryRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // async task: connect with EmployeeSalaryRepo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSalary>>> Get()
        {
            return await _repository.GetSalaries();
        }

    }
}
