namespace LoLWC.Web.ViewModels.Home
{
    using LoLWC.Data.Models;
    using LoLWC.Web.Infrastructure.Mapping;

    public class JokeCategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
