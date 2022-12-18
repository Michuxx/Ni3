namespace back_end.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int CourseId{ get; set; }

        public Account Account { get; set; }
        public Course Course { get; set; }
    }
}
