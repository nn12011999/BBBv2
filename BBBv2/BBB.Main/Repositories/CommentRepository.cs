using BBB.Data;
using BBB.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BBB.Main.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Comment> GetAll()
        {
            return _context.Comments.ToList();
        }

        public Comment GetById(int id)
        {
            return _context.Comments.Find(id);
        }

        public IList<Comment> GetByPostId(int id)
        {
            return _context.Comments.Where(c => c.PostId == id).ToList();
        }

        public IList<Comment> GetByUserId(int id)
        {
            return _context.Comments.Where(c => c.UserId == id).ToList();
        }
    }
}
