namespace BBB.Data.DataModel.Request
{
    public class UpdateCommentRequest
    {
        public int CommentId { get; set; }
        public string Context { get; set; }
        public int UserId { get; set; }

    }
}
