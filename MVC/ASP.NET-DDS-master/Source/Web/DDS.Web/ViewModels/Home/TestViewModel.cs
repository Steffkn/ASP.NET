namespace DDS.Web.ViewModels.Home
{
    using Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class TestViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        public int[] SelectedTags { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}