using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Response
{
    public class GetCommentOfPostResponse
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Context { get; set; }
    }
}
