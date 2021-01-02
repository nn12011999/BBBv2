using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class AddPostRequest
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
    }
}
