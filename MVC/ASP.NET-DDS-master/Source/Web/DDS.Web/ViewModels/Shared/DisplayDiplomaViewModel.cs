namespace DDS.Web.ViewModels.Shared
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Data.Models;

    public class DisplayDiplomaViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Експериментална част")]
        public string ExperimentalPart { get; set; }

        [Display(Name = "Съдържание")]
        public IList<string> ContentCSV { get; set; }

        public int[] TagsID { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        public int TeacherID { get; set; }

        [Display(Name = "Ръководител")]
        public string TeacherName { get; set; }

        [Display(Name = "Удобрена от ръководител")]
        public bool ApprovedByLeader { get; set; }

        [Display(Name = "Удобрена от канцелария")]
        public bool ApprovedByHead { get; set; }

        [Display(Name = "Създадена")]
        public string CreatedOn { get; set; }

        [Display(Name = "Последно променена")]
        public string ModifiedOn { get; set; }

        [Display(Name = "Изтрита на:")]
        public string DeletedOn { get; set; }

        [Display(Name = "Is deleted?")]
        public bool IsDeleted { get; set; }
    }
}
