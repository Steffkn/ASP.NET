namespace ElementsWeb.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using ElementsWeb.Data.Common.Models;

    public class Character : BaseModel<int>
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [MinLength(3, ErrorMessage = "Username should be at least 3 characters long.")]
        public string Name { get; set; }

        public int RandomNumber { get; set; }

        public int MaxHealth { get; set; }

        public int MaxResource { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
