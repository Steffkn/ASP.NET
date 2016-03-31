namespace Blog.Services.Data
{
    using System.Linq;
    using Blog.Data.Models;

    public interface IPostCategoriesService
    {
        IQueryable<PostCategory> GetAll();

        PostCategory EnsureCategory(string name);
    }
}
