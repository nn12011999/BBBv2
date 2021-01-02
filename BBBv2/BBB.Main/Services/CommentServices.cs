using BBB.Data;
using BBB.Data.Entities;
using System;

namespace BBB.Main.Services
{
    public class CommentServices : ICommentServices
    {
        ApplicationDbContext _context;
        CommentServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public string AddComment(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteComment(Comment comment)
        {
            try
            {
                _context.Comments.Remove(comment);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateComment(Comment comment)
        {
            try
            {
                _context.Comments.Update(comment);
                var response = _context.SaveChanges();
                if (response < 1)
                {
                    return "Cannot execute. Plz contact Admin";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
