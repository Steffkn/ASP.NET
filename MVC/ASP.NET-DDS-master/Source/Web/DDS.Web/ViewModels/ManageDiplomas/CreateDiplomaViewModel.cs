namespace DDS.Web.ViewModels.ManageDiplomas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

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

        [Required]
        [Display(Name = "Експериментална част")]
        public string ExperimentalPart { get; set; }

        [Display(Name = "Съдържание")]
        public IList<string> ContentCSV { get; set; }

        public string[] TagsNames { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        public int TeacherID { get; set; }

        [Display(Name = "Ръководител")]
        public string TeacherName { get; set; }
    }
}
