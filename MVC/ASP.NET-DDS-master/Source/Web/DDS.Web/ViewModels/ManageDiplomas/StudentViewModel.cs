namespace DDS.Web.ViewModels.ManageDiplomas
{
    using AutoMapper;

    using DDS.Data.Models;
    using DDS.Web.Infrastructure.Mapping;

    public class StudentViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        //[Required]
        //public int Id { get; set; }

        //public int FNumber { get; set; }

        //public string Address { get; set; }

        public string User_Id { get; set; }

        //public Diploma SelectedDiploma { get; set; }

        //public bool IsGraduate { get; set; }

        public string ScienceDegree { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Student, StudentViewModel>()
                .ForMember(x => x.User_Id, opt => opt.Ignore());
            configuration.CreateMap<Student, StudentViewModel>()
                .ForMember(x => x.ScienceDegree, opt => opt.Ignore());
            configuration.CreateMap<Student, StudentViewModel>()
                .ForMember(x => x.Email, opt => opt.Ignore());
            configuration.CreateMap<Student, StudentViewModel>()
                .ForMember(x => x.PhoneNumber, opt => opt.Ignore());
        }

        //public string UserName { get; set; }
    }
}
