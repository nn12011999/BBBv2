using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface ICommentRepository
    {
        IList<Comment> GetAll();
        IList<Comment> GetByPostId(int id);
        IList<Comment> GetByUserId(int id);
    }
}
