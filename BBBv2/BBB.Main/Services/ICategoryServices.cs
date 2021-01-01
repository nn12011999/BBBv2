using BBB.Data.Entities;

namespace BBB.Main.Services
{
    public interface ICategoryServices
    {
        public string AddCategory(Category category);
        public string DeleteCategory(Category category);
        public string UpdateCategory(Category category);
    }
}
