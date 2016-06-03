namespace DDS.Web.ViewModels.ManageDiplomas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using DDS.Data.Models;
    using DDS.Web.Infrastructure.Mapping;

    public class EditDiplomaViewModel : IMapTo<Diploma>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Content")]
        public IList<string> ContentCSV { get; set; }

        [Required]
        [Display(Name = "Experimental part")]
        public string ExperimentalPart { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public bool ApprovedByLeader { get; set; }

        public bool ApprovedByHead { get; set; }
    }
}
