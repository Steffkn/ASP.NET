namespace Blog.Services.Data
{
    using System;
    using System.Linq;
    using Blog.Data.Common;
    using Blog.Data.Models;

    public class PostService : IPostService
    {
        private readonly IDbRepository<Post> posts;

        public PostService(IDbRepository<Post> posts)
        {
            this.posts = posts;
        }

        public Post GetById(int id)
        {
            var post = this.posts.GetById(id);
            return post;
        }

        public IQueryable<Post> GetRandomPost(int count)
        {
            return this.posts.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }

        public IQueryable<Post> GetAllPost()
        {
            return this.posts.All().OrderBy(x => x.Id);
        }
    }
}
