namespace DDS.Web.ViewModels.ManageDiplomas
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class CommonDiplomaViewModel : IMapFrom<Diploma>
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
    }
}
