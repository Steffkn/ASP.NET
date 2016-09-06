namespace DDS.Web.ViewModels.Shared
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using System;
    public class CommonDiplomaViewModel : IMapFrom<Diploma>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Тема")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Експериментална част")]
        public string ExperimentalPart { get; set; }

        [Required]
        [Display(Name = "Създадена на")]
        public string CreatedOn { get; set; }

        [Required]
        [Display(Name = "Модифицирана на")]
        public DateTime? ModifiedOn { get; set; }

        [Required]
        [Display(Name = "Избрана")]
        public bool IsSelectedByStudent { get; set; }

        [Required]
        [Display(Name = "Одобрена от ръководителя")]
        public bool IsApprovedByLeader { get; set; }

        [Required]
        [Display(Name = "Одобрена от декан")]
        public bool IsApprovedByHead { get; set; }

        [Required]
        [Display(Name = "Ръководител")]
        public string TeacherName { get; set; }

        public ICollection<Tag> Tags { get; set; }

        [Required]
        public int TeacherID { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            // string.Format is not recognised so this happens
            configuration.CreateMap<Diploma, CommonDiplomaViewModel>()
                .ForMember(x => x.TeacherName, opt => opt.MapFrom(x => (x.Teacher.User.ScienceDegree + " " + x.Teacher.User.FirstName + " " + x.Teacher.User.LastName).Trim()));
            configuration.CreateMap<Diploma, CommonDiplomaViewModel>()
                .ForMember(x => x.TeacherID, opt => opt.MapFrom(x => x.Teacher.Id));
        }
    }
}
