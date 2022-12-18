namespace back_end.Models
{
    public class AddCommentDto
    {
        public string Text { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
