using back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

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
        [HttpGet("courses/All")]
        public async Task<ActionResult<List<AddCourseDto>>> GetCourseList()
        {
            List<AddCourseDto> courses = new List<AddCourseDto>();

            // rewrite courses to simpler class
            foreach (Course rewriter in await _context.Courses.ToListAsync())
            {
                AddCourseDto temp = new AddCourseDto();
                temp.CourseName = rewriter.CourseName;
                temp.Description = rewriter.Description;
                courses.Add(temp);
            }

            return Ok(courses);
        }

    
        [HttpPost("comments/{courseName}")]
        public async Task<ActionResult<AddCommentDto>> AddComment([FromBody] AddCommentDto comment, string courseName)
        {
            Comment newComment = new Comment();

            newComment.CourseId = -1;
            // find course id
            foreach (Course course in await _context.Courses.ToListAsync())
            {
                if (course.CourseName == courseName)
                {
                    newComment.CourseId = course.Id;
                }
            }

            if (newComment.CourseId == -1)
                return NotFound("Course not found");

            newComment.UserId = -1;
            // find user id
            foreach (Account user in await _context.Accounts.ToListAsync())
            {
                if (user.Login == comment.UserLogin)
                {
                    newComment.UserId = user.Id;
                }
            }
            if (newComment.UserId == -1)
                return NotFound("Account not found");

            newComment.Text = comment.Text;

            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();
            return Ok(comment);
        }
        
        
        [HttpGet("comments/{courseName}")]
        public async Task<ActionResult<List<GetCommentDto>>> GetComments(string courseName)
        {
            // find id of a course 
            int courseId = -1;
            foreach (Course course in await _context.Courses.ToListAsync())
            {
                if (course.CourseName == courseName)
                {
                    courseId = course.Id;
                }
            }
            if (courseId == -1)
                return NotFound("course not found");


            // create a list with a course comments
            List<Account> accounts = await _context.Accounts.ToListAsync();
            List<Comment> comments = await _context.Comments.ToListAsync();
            List<GetCommentDto> courseComments = new List<GetCommentDto>();

            // select comments
            foreach (Comment comment in comments)
            {
                if (comment.CourseId == courseId)
                {
                    GetCommentDto commentDto = new GetCommentDto();
                    commentDto.Text = comment.Text;
                    commentDto.UserLogin = "Anonim";    // in case there is an account missing

                    // find login
                    foreach (Account account in accounts)
                    {
                        if (comment.UserId == account.Id)
                        {
                            commentDto.UserLogin = account.Login;
                        }
                    }
                    courseComments.Add(commentDto);
                }
            }

            if (courseComments.Count == 0)
                return NotFound("Did not find comment to a requested course");

            return Ok(courseComments);
        }

    }
}
