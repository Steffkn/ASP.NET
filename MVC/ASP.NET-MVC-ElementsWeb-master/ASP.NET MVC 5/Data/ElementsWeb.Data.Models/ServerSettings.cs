namespace ElementsWeb.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class ServerSettings : BaseModel<int>
    {
        [Required(ErrorMessage = "Version is required")]
        public string Version { get; set; }

        [Required(ErrorMessage = "URL is required")]
        public string URL { get; set; }
    }
}
