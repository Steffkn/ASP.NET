namespace Blog.Web.ViewModels.Home
{
    using Blog.Data.Models;
    using Blog.Web.Infrastructure.Mapping;

    public class JokeCategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
