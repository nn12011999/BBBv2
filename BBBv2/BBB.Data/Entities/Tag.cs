﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBB.Data.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
