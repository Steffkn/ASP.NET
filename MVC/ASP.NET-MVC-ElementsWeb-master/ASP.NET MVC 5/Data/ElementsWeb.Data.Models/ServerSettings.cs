namespace ElementsWeb.Data.Models
{
    using ElementsWeb.Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class ServerSettings : BaseModel<int>
    {
        [Required(ErrorMessage = "Version is required")]
        public string Version { get; set; }

        [Required(ErrorMessage = "URL is required")]
        public string URL { get; set; }
    }
}
