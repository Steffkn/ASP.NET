namespace DDS.Web.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>, IHaveCustomMappings
    {
        [Required]
        public string id { get; set; }

        [Required]
        public string text { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Tag, TagViewModel>()
                .ForMember(x => x.text, opt => opt.MapFrom(x => x.Name));
            configuration.CreateMap<Tag, TagViewModel>()
                .ForMember(x => x.id, opt => opt.MapFrom(x => x.Id));
        }
    }
}