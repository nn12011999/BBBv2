﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BBB.Data.DataModel.Request
{
    public class UpdatePostRequest
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
    }
}
