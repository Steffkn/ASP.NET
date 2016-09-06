namespace DDS.Web.ViewModels.Shared
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Areas.Administration.ViewModels;
    public class DisplayDiplomaViewModel : IMapFrom<Diploma>, IHaveCustomMappings
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

        [Display(Name = "Съдържание")]
        public IList<string> ContentCSV { get; set; }

        public string[] TagsNames { get; set; }

        [Display(Name = "Категории")]
        public IEnumerable<SelectListItem> Tags { get; set; }

        public int TeacherID { get; set; }

        [Display(Name = "Ръководител")]
        public string TeacherName { get; set; }

        [Display(Name = "Одобрена от ръководител")]
        public bool IsApprovedByLeader { get; set; }

        [Display(Name = "Одобрена от канцелария")]
        public bool IsApprovedByHead { get; set; }

        [Display(Name = "Избрана от студент")]
        public bool IsSelectedByStudent { get; set; }

        [Display(Name = "Създадена")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Последно променена")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Изтрита на:")]
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "Изтрита?")]
        public bool IsDeleted { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Diploma, DisplayDiplomaViewModel>()
                .ForMember(src => src.ContentCSV, dest => dest.Ignore())
                .ForMember(src => src.Tags, dest => dest.Ignore());
        }
    }
}
