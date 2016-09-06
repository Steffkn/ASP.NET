namespace DDS.Web.Areas.Administration.ViewModels
{
    using DDS.Data.Models;
    using DDS.Web.Infrastructure.Mapping;

    public class DiplomaTitleViewModel : IMapFrom<Diploma>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
