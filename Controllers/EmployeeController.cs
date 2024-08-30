using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartWay_Test_Task.Entities;
using SmartWay_Test_Task.Entities.Dtos;
using SmartWay_Test_Task.Interfaces;
using SmartWay_Test_Task.utl;

namespace SmartWay_Test_Task.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //get id +
        //get dep +
        //get all +

        //delete +
        //post +
        //update

        private readonly IRepository<Employee> _repository;

        public EmployeeController(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> Get([FromRoute]int id)
        {
            var employee = await _repository.Get(id);
            if(employee != null)
                return Ok(employee.ToResponseDto());
            return NotFound(id);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IQueryable<EmployeeResponseDto>>> GetAll()
        {
            var employees = await _repository.GetAll();
            if (employees.Any())
                return Ok(employees.Select(e => e.ToResponseDto()));
            return NotFound();
        }

        [HttpGet("getbydep/{depname}")]
        public async Task<ActionResult<IQueryable<EmployeeResponseDto>>> GetAll([FromRoute]string depName)
        {
            var employees = await _repository.GetAll(e=>e.Department.Name == depName);
            if(employees.Any())
                return Ok(employees.Select(e => e.ToResponseDto()));
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            var employee = await _repository.Get(id);
            if( employee == null )
                return NoContent();
            return BadRequest(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(EmployeeRequestDto employee)
        {
            int? newid = await _repository.Add(employee.ToEmployee());
            if (newid != null)
                return Created("",newid);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EmployeeRequestDto employee)
        {
            await _repository.Update(id, employee.ToEmployee());
            return NoContent();
        }
    }
}
