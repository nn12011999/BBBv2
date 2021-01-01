using BBB.Data.Entities;
using System.Linq;

namespace BBB.Data.DataModel
{
    public class SeedData
    {
        public static void SeedUserData(ApplicationDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                var admin = new User
                {
                    UserName = "admin",
                    Password = "123",
                    Role = RoleDefine.Admin
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }

        public static void SeedCategoryData(ApplicationDbContext context)
        {
            if (context.Categories.Count() == 0)
            {
                var news = new Category
                {
                    Name = "News",
                    ParentId = null,
                };

                context.Categories.Add(news);
                context.SaveChanges();
            }
        }
    }
}
