namespace Blog.Data.Models
{
    using System.Collections.Generic;

    using Blog.Data.Common.Models;

    public class PostCategory : BaseModel<int>
    {
        public PostCategory()
        {
            this.Posts = new HashSet<Post>();
        }

        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
