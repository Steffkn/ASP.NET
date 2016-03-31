namespace Blog.Data.Models
{
    using Blog.Data.Common.Models;

    public class Post : BaseModel<int>
    {
        public string Content { get; set; }

        public string AuthorID { get; set; }

        public int CategoryId { get; set; }

        public virtual PostCategory Category { get; set; }

        public string ImageURL { get; set; }
    }
}
