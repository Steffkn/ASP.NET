namespace DDS.Web.ViewModels.ManageDiplomas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateDiplomaViewModel : IMapFrom<Diploma>
    {
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
    }
}
