namespace ElementsWeb.Web.ViewModels.API
{
    using System.ComponentModel.DataAnnotations;
    using ElementsWeb.Data.Models;
    using ElementsWeb.Web.Infrastructure.Mapping;

    public class SimpleCharacterViewModel : IMapFrom<Character>
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public long CurentExperiense { get; set; }
    }
}
