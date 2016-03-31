namespace Blog.Services.Data
{
    using System.Linq;
    using Blog.Data.Common;
    using Blog.Data.Models;

    public class PostCategoriesService : IPostCategoriesService
    {
        private readonly IDbRepository<PostCategory> postCategories;

        public PostCategoriesService(IDbRepository<PostCategory> categories)
        {
            this.postCategories = categories;
        }

        public PostCategory EnsureCategory(string name)
        {
            var category = this.postCategories.All().FirstOrDefault(x => x.Name == name);
            if (category != null)
            {
                return category;
            }

            category = new PostCategory { Name = name };
            this.postCategories.Add(category);
            this.postCategories.Save();
            return category;
        }

        public IQueryable<PostCategory> GetAll()
        {
            return this.postCategories.All().OrderBy(x => x.Name);
        }
    }
}
