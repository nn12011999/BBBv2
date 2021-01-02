using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface ICommentRepository
    {
        IList<Comment> GetAll();
        IList<GetCommentOfPostByPostId> GetByPostId(int id);
        IList<Comment> GetByUserId(int id);
        Comment GetById(int id);
    }
}
