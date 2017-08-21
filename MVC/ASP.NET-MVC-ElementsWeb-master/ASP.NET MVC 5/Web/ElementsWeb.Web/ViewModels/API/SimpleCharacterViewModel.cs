namespace ElementsWeb.Web.ViewModels.API
{
    using ElementsWeb.Data.Models;
    using ElementsWeb.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class SimpleCharacterViewModel : IMapFrom<Character>
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "MaxHealth")]
        public int MaxHealth { get; set; }

        [Required]
        [Display(Name = "MaxResource")]
        public int MaxResource { get; set; }

        //[Required]
        //[Display(Name = "CharacterType")]
        //public CharacterType CharacterType { get; set; }
    }

    //public enum CharacterType
    //{
    //    Default,
    //    FuseModel,
    //    Kachujin
    //}
}
