using BBB.Data;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public IList<GetCommentOfPostResponse> GetByPostId(int id)
        {
            return _context.Comments.Where(c => c.PostId == id)
                .Include(c => c.User)
                .OrderBy(x => x.TimeStamp)
                .Select(x=>new GetCommentOfPostResponse {
                    Id = x.Id,
                    UserId = x.UserId,
                    PostId = x.PostId,
                    Context = x.Context,
                    UserName = x.User.UserName,
                    TimeStamp = x.TimeStamp.ToString("dd/MM/yyyy")
                }).ToList();
        }

        public IList<GetCommentOfPostResponse> GetByPostUrl(string url)
        {
            return _context.Comments
                .Include(c => c.User)
                .Include(p => p.Post)
                .OrderBy(x => x.TimeStamp)
                .Where(x => x.Post.Url == url)
                .Select(x => new GetCommentOfPostResponse
                {
                   Id = x.Id,
                   UserId = x.UserId,
                   PostId = x.PostId,
                   Context = x.Context,
                   UserName = x.User.UserName,
                   TimeStamp = x.TimeStamp.ToString("dd/MM/yyyy")
                }).ToList();
        }

        public IList<Comment> GetByUserId(int id)
        {
            return _context.Comments.Where(c => c.UserId == id)
                .Include(c => c.User).ToList();
        }
    }
}
