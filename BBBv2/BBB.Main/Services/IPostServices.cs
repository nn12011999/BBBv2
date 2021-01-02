using BBB.Data.Entities;

namespace BBB.Main.Services
{
    public interface IPostServices
    {
        public string AddPost(Post Post);
        public string DeletePost(Post Post);
        public string UpdatePost(Post Post);

    }
}
