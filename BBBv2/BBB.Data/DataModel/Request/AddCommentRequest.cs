namespace BBB.Data.DataModel.Request
{
    public class AddCommentRequest
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Context { get; set; }
    }
}
