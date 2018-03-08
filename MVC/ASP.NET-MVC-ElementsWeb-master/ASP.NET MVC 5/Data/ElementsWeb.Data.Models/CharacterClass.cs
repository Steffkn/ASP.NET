namespace ElementsWeb.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class CharacterClass : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
