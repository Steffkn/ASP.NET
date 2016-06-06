namespace DDS.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Е-майл")]
        public string Email { get; set; }
    }
}
