using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.DatabaseFirst;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DbFirst _context;

        public EmployeeController(DbFirst dataContext)
        {
            _context = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Employee>> Get(int ID)
        {
            var employee = await _context.Employees.FindAsync(ID);
            if (employee == null)
                return BadRequest("employee not found.");
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync()); ;
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee request)
        {
            var db = await _context.Employees.FindAsync(request.Id);
            if (db == null)
                return BadRequest("Hero not found.");

            db.Name = request.Name;
            db.Salary = request.Salary;
            db.Address = request.Address;

            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult<List<Employee>>> Delete(int ID)
        {
            var dbaccount = await _context.Employees.FindAsync(ID);
            if (dbaccount == null)
                return BadRequest("Employee not found.");

            _context.Employees.Remove(dbaccount);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
        }
    }
}
