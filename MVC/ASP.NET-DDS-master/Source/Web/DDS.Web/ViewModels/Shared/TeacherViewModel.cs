namespace DDS.Web.ViewModels.Shared
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;

    using Data.Models;
    using DDS.Web.Infrastructure.Mapping;

    public class TeacherViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Преподавател")]
        public string TeacherName { get; set; }

        [Display(Name = "Емайл")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        public string ScienceDegree { get; set; }

        [Display(Name = "Дисциплини")]
        public ICollection<Tag> Tags { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            // string.Format is not recognised so this happens
            configuration.CreateMap<Teacher, TeacherViewModel>()
                .ForMember(x => x.TeacherName, opt => opt.MapFrom(x => x.User.ScienceDegree + " " + x.User.FirstName + " " + x.User.LastName));

            configuration.CreateMap<Teacher, TeacherViewModel>()
                .ForMember(x => x.ScienceDegree, opt => opt.MapFrom(x => x.User.ScienceDegree));

            configuration.CreateMap<Teacher, TeacherViewModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email));

            configuration.CreateMap<Teacher, TeacherViewModel>()
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber));
        }
    }
}
