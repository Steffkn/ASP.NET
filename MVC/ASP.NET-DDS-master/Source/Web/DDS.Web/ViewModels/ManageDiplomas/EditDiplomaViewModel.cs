namespace DDS.Web.ViewModels.ManageDiplomas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using DDS.Data.Models;
    using DDS.Web.Infrastructure.Mapping;

    public class EditDiplomaViewModel : IMapTo<Diploma>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Тема")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Съдържание")]
        public IList<string> ContentCSV { get; set; }

        [Required]
        [Display(Name = "Експериментална част")]
        public string ExperimentalPart { get; set; }

        public ICollection<Tag> Tags { get; set; }

        [Required]
        [Display(Name = "Избрана")]
        public bool IsSelectedByStudent { get; set; }

        [Required]
        [Display(Name = "Удобрена от ръководителя")]
        public bool IsApprovedByLeader { get; set; }

        [Required]
        [Display(Name = "Удобрена от декан")]
        public bool IsApprovedByHead { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Diploma, EditDiplomaViewModel>()
                    .ForMember(src => src.ContentCSV, dest => dest.Ignore())
                    .ForMember(src => src.Tags, dest => dest.Ignore());
        }
    }
}
