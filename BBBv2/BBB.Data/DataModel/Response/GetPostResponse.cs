namespace BBB.Data.DataModel.Response
{
    public class GetPostResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Url { get; set; }
        public string TimeStamp { get; set; }
    }
}
