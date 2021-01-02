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
                    Editable = false,
                    Slug = "News-1"
                };

                var blogs = new Category
                {
                    Name = "Blogs",
                    ParentId = null,
                    Editable = false,
                    Slug = "Blogs-2"
                };

                var web_info = new Category
                {
                    Name = "WebInfo",
                    ParentId = null,
                    Editable = false,
                    Slug = "WebInfo-3"
                };

                var video = new Category
                {
                    Name = "Video",
                    ParentId = null,
                    Editable = false,
                    Slug = "Video-4"
                };

                context.Categories.Add(news);
                context.Categories.Add(blogs);
                context.Categories.Add(web_info);
                context.Categories.Add(video);
                context.SaveChanges();
            }
        }
    }
}
