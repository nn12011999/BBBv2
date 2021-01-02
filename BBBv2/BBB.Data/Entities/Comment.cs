using System;
using System.ComponentModel.DataAnnotations;

namespace BBB.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int UserId { get; set; }
        public virtual User User{get;set;}
        public string Context { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
