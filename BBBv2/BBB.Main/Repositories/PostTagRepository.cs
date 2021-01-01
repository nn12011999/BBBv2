using BBB.Data;
using BBB.Data.Entities;
using System.Collections.Generic;

namespace BBB.Main.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        ApplicationDbContext _context;

        PostTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<PostTag> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IList<PostTag> GetByPostId(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<PostTag> GetByTagId(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
