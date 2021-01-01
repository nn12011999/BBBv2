using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public interface IPostTagRepository
    {
        IList<PostTag> GetAll();
        IList<PostTag> GetByPostId(int id);
        IList<PostTag> GetByTagId(int id);
    }
}
