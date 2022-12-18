namespace back_end.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty ;

        public List<Comment> Comments { get; set; }
    }
}
