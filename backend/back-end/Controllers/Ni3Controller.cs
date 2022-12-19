using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ni3Controller : ControllerBase
    {
       
        private readonly DataContext _context;
        public Ni3Controller(DataContext context)
        {
            _context = context;
        }

       

        [HttpPost("courses")]
        public async Task<Course> AddCourse([FromBody] AddCourseDto addCourseDto)
        {
            var course = new Course()
            {
                CourseName = addCourseDto.CourseName,
                Description = addCourseDto.Description
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course;
        }

       
        [HttpPost("accounts")]
        public async Task<Account> AddAccountInside([FromBody] AddAccountDto addAccountDto)
        {
            var accounts = new Account()
            {
                Login = addAccountDto.Login,
                Password = addAccountDto.Password
            };

            await _context.Accounts.AddAsync(accounts);
            await _context.SaveChangesAsync();
            return accounts;
           
        }

        [HttpPost("accounts/Check")]
        public async Task<ActionResult<Account>> CheckIsAccountValid([FromBody] AddAccountDto addAccountDto)
        {
            var account = new Account()
            {
                Login = addAccountDto.Login,
                Password = addAccountDto.Password
            };

            var records = await _context.Accounts.SingleOrDefaultAsync(x => x.Login == account.Login);
            

            if (records == null)
            {
                return NotFound();
            }
           
            if (addAccountDto.Password != records.Password)
            {
                return NotFound();
            }


            records.Password = null;
            return Ok(records);
        }

        [HttpPost("accounts/New")]
        public async Task<ActionResult<Account>> AddAccount([FromBody] AddAccountDto addAccountDto)
        {
            // check if there is an account with posing login
            foreach (Account checkerAccount in await _context.Accounts.ToListAsync())
            {
                if (checkerAccount.Login == addAccountDto.Login)
                {
                    return BadRequest("There is an account with login alike posted");
                }
            }
            var account = new Account()
            {
                Login = addAccountDto.Login,
                Password = addAccountDto.Password
            };

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return Ok();

        }

    }
}
