namespace DDS.Web.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class SimpleStudentViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = "Фак. номер")]
        public int FNumber { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Завършил")]
        public bool IsGraduate { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Научна степен")]
        public string ScienceDegree { get; set; }

        public string UserID { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Student, SimpleStudentViewModel>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.User.FirstName));
            configuration.CreateMap<Student, SimpleStudentViewModel>()
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.User.LastName));
            configuration.CreateMap<Student, SimpleStudentViewModel>()
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(x => x.User.MiddleName));
            configuration.CreateMap<Student, SimpleStudentViewModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email));
            configuration.CreateMap<Student, SimpleStudentViewModel>()
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.User.PhoneNumber));
            configuration.CreateMap<Student, SimpleStudentViewModel>()
                .ForMember(x => x.ScienceDegree, opt => opt.MapFrom(x => x.User.ScienceDegree));
            configuration.CreateMap<Student, SimpleStudentViewModel>()
                .ForMember(x => x.UserID, opt => opt.MapFrom(x => x.User.Id));
        }
    }
}
