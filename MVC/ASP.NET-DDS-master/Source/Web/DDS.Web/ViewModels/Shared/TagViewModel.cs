namespace DDS.Web.ViewModels.Shared
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class TagViewModel : IMapFrom<Tag>
    {
        [Required]
        public string id { get; set; }

        [Required]
        public string text { get; set; }
    }
}