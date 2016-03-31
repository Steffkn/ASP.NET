namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Blog.Common;
    using Blog.Data.Models;
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Reflection;
    using System.IO;
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        // get all images in the directory for populating in db
        public string[] GetImagesInDir()
        {
            string imagePath = @"\Source\Web\Blog.Web\Content\Images\";

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string pathToAssembly = Path.GetDirectoryName(path);
            var index = pathToAssembly.LastIndexOf(@"\Source\");

            string[] filePaths = Directory.GetFiles(pathToAssembly.Substring(0, index) + imagePath);

            for (int i = 0; i < filePaths.Count(); i++)
            {
                Console.WriteLine(filePaths[i]);
            }

            return filePaths;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            const string AdministratorUserName = "admin@admin.com";
            const string AdministratorPassword = AdministratorUserName;

            if (!context.Roles.Any())
            {
                // Create admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(role);

                // Create admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser { UserName = AdministratorUserName, Email = AdministratorUserName };
                userManager.Create(user, AdministratorPassword);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
            }

            if (!context.PostsCategories.Any())
            {
                IDbRepository<PostCategory> categories = new DbRepository<PostCategory>(context);
                categories.Add(new PostCategory() { Name = "C#" });
                categories.Add(new PostCategory() { Name = "Javascript" });
                categories.Add(new PostCategory() { Name = "Java" });
                categories.Add(new PostCategory() { Name = "HTML" });
                categories.Add(new PostCategory() { Name = "CSS" });
                categories.Add(new PostCategory() { Name = "Database" });

                categories.Save();
            }

            if (!context.Posts.Any())
            {
                IDbRepository<Post> posts = new DbRepository<Post>(context);
                string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.Phasellus quis tempor ante, in mattis augue. Phasellus in velit quis orci convallis sodales.Maecenas at eros a erat aliquet efficitur sit amet sit amet dui. Proin lorem eros, dapibus eget orci ac, sagittis pharetra ante. Praesent mollis odio fringilla vehicula ultricies. Nam at suscipit nisl. Phasellus fringilla feugiat porttitor. Praesent non orci in lacus dignissim sagittis.Mauris cursus eleifend convallis. Maecenas vitae nunc nec sapien vulputate suscipit.Suspendisse aliquam lacinia fermentum. Curabitur pellentesque neque eu ultrices rutrum. Vivamus at tempus quam. Integer non mauris ex. Nullam in mi sem.";

                var categoriesCount = context.PostsCategories.Count();

                for (int i = 0; i < 100; i++)
                {
                    Random randomGenerator = new Random(i);
                    var start = randomGenerator.Next(i * 3);
                    var end = randomGenerator.Next(i * 3, loremIpsum.Length - 1);
                    var categoryID = randomGenerator.Next(1, categoriesCount);

                    var post = new Post()
                    {
                        AuthorID = context.Users.First().Id,
                        Category = context.PostsCategories.FirstOrDefault(c => c.Id == categoryID),
                        Content = loremIpsum.Substring(start, end - start).Trim()
                    };

                    posts.Add(post);
                }

                posts.Save();
            }
        }
    }
}
