namespace Blog.Services.Data
{
    using System.Linq;
    using Blog.Data.Models;

    public interface IPostService
    {
        IQueryable<Post> GetRandomPost(int count);

        IQueryable<Post> GetAllPost();

        Post GetById(int id);
    }
}
