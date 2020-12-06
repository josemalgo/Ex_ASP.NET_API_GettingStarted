using System;
using System.Collections.Generic;
using Employees.Models;
using Employees.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employees.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            EmployeeDbContext repo = new EmployeeDbContext();
            var allEmployees = repo.GetAllEmployees();
            
            if(allEmployees != null)
                return Ok(allEmployees);

            return NotFound();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(Guid id)
        {
            EmployeeDbContext repo = new EmployeeDbContext();
            var entity = repo.GetEmployeeById(id);
            
            if(entity != null)
                return Ok(entity);

            return NotFound();
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public ActionResult Post([FromBody] Employee newEmployee)
        {
            EmployeeDbContext repo = new EmployeeDbContext();
            var output = repo.Create(newEmployee);
            
            if(output.IsSuccess)
                return CreatedAtAction(nameof(Get), new { id = output.ValidatedResult.Id }, output.ValidatedResult);

            return NoContent();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Employee employee)
        {
            EmployeeDbContext repo = new EmployeeDbContext();
            var output = repo.Update(id, employee);

            if (output.IsSuccess)
                return Ok();

            return NotFound();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> Delete(Guid id)
        {
            EmployeeDbContext repo = new EmployeeDbContext();
            var output = repo.Delete(id);

            if (output.IsSuccess)
                return Ok();

            return NotFound();
        }
    }
}
