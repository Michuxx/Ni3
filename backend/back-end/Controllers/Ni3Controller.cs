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
        public async Task<Account> AddAccount([FromBody] AddAccountDto addAccountDto)
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



            return Ok();
        }

    }
}
