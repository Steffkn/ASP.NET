namespace DDS.Web.ViewModels.Diplomas
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class DiplomaViewModel : IMapFrom<Diploma>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}