using BBB.Data.Entities;

namespace BBB.Main.Services
{
    public interface ICommentServices
    {
        string AddComment(Comment comment);
        string UpdateComment(Comment comment);
        string DeleteComment(Comment comment);

    }
}
