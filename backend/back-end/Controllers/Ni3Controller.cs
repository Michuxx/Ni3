using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ni3Controller : ControllerBase
    {
        // lists created for testing purposes
        //private static List<Course> courses = new List<Course>
        //{
        //    new Course
        //        {
        //            Id = 1,
        //            CourseName = "Złożoność Obliczeniowa i Struktury Danych",
        //            Description = "Creme de la crem Informatyki"
        //        }
        //};
        //private static List<Comment> comments = new List<Comment>
        //{
        //    new Comment
        //    {
        //        Id = 2,
        //        Text = "Kapłon to potężny prowadzący",
        //        UserId = 1,
        //        CourseId = 1
        //    }
        //};
        //private static List<Account> accounts = new List<Account>
        //{
        //    new Account
        //    {
        //        Id = 10,
        //        Login = "Login",
        //        Password = "Password"

        //    }
        //};
        private readonly DataContext _context;
        public Ni3Controller(DataContext context)
        {
            _context = context;
        }

        // controller code section for Course class api traffic handling
        [HttpGet("courses")]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            return Ok(await _context.Courses.ToListAsync());
        }

        [HttpGet("courses/{id:int}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound("Course not found");
            return Ok(course);
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


        [HttpPut("courses/{id:int}")]
        public async Task<ActionResult<List<Course>>> UpdateCourse(Course request, int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound("Course to update not found");
            
            course.CourseName = request.CourseName;
            course.Description = request.Description;

            await _context.SaveChangesAsync();

            return Ok(_context.Courses.ToListAsync());
        }

        [HttpDelete("courses/{id:int}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound("Course to delete not found");

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return Ok(await _context.Courses.ToListAsync());
        }


        // controller section for Comment class api traffic handling
        [HttpGet("comments")]
        public async Task<ActionResult<List<Comment>>> GetComments()
        {
            return Ok(await _context.Comments.ToListAsync());
        }

        [HttpPost("comments")]
        public async Task<ActionResult<List<Comment>>> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpDelete("comments/{id:int}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return BadRequest("Comment to delete not found");
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }


        // controller section for Account class api traffic handling
        [HttpGet("accounts")]
        public async Task<ActionResult<List<Account>>> GetAccounts()
        {
            return Ok(await _context.Accounts.ToListAsync());
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
