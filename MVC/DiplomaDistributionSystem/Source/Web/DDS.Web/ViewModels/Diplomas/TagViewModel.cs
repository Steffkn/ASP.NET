namespace DDS.Web.ViewModels.Diplomas
{
    using DDS.Data.Models;
    using DDS.Web.Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public string Name { get; set; }
    }
}
