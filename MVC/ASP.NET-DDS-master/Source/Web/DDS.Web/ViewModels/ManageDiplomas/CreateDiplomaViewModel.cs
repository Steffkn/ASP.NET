namespace DDS.Web.ViewModels.ManageDiplomas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CreateDiplomaViewModel : IMapFrom<Diploma>
    {
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
    }
}
