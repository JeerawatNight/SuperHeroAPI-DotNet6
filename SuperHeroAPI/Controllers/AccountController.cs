using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<Account>> Get(string username)
        {
            var account = await _context.Accounts.FindAsync(username);
            if (account == null)
                return BadRequest("Hero not found.");
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> AddHero(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.ToListAsync()); ;
        }

        [HttpPut]
        public async Task<ActionResult<List<Account>>> UpdateHero(Account request)
        {
            var dbaccount = await _context.Accounts.FindAsync(request.username);
            if (dbaccount == null)
                return BadRequest("Hero not found.");

            dbaccount.password = request.password;
            dbaccount.Firstname = request.Firstname;
            dbaccount.Lastname = request.Lastname ;
            dbaccount.email = request.email;

            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpDelete("{username}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(string username)
        {
            var dbaccount = await _context.Accounts.FindAsync(username);
            if (dbaccount == null)
                return BadRequest("Hero not found.");

            _context.Accounts.Remove(dbaccount);
            await _context.SaveChangesAsync();

            return Ok(await _context.Accounts.ToListAsync());
        }
    }
}
