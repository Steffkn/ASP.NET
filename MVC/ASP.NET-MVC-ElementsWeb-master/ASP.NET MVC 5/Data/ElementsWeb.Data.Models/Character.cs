namespace ElementsWeb.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Character : BaseModel<int>
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [MinLength(3, ErrorMessage = "Username should be at least 3 characters long.")]
        public string Name { get; set; }

        [DefaultValue(1)]
        public int Level { get; set; }

        [DefaultValue(0)]
        public long CurentExperiense { get; set; }

        public string Location { get; set; }

        public string Rotation { get; set; }

        public virtual CharacterClass Class { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
