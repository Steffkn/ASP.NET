namespace ElementsWeb.Web.ViewModels.Home
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using ElementsWeb.Data.Models;
    using ElementsWeb.Web.Infrastructure.Mapping;

    public class CharacterViewModel : IMapFrom<Character>, IHaveCustomMappings
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string UserId { get; set; }

        [Display(Name = "User")]
        public string Username { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Character, CharacterViewModel>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.UserName));
        }
    }
}
