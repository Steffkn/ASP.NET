namespace DDS.Web.ViewModels.Tags
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class SelectTagViewModel : IMapFrom<Tag>, IMapTo<Tag>, IHaveCustomMappings
    {
        [Required]
        public string id { get; set; }

        [Required]
        public string text { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Tag, SelectTagViewModel>()
                .ForMember(x => x.id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.text, opt => opt.MapFrom(x => x.Name));

            int value = 0;
            configuration.CreateMap<SelectTagViewModel, Tag>()
                .ForMember(s => s.Name, opt => opt.MapFrom(d => d.text))
                .ForMember(s => s.Id, opt => opt.MapFrom(d => int.TryParse(d.id, out value)));
        }
    }
}
