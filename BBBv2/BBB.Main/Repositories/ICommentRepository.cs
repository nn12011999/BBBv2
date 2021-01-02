using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface ICommentRepository
    {
        IList<Comment> GetAll();
        IList<GetCommentOfPostResponse> GetByPostId(int id);
        IList<GetCommentOfPostResponse> GetByPostUrl(string url);
        IList<Comment> GetByUserId(int id);
        Comment GetById(int id);
    }
}
