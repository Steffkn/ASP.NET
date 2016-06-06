namespace DDS.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде поне {2} символа!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("NewPassword", ErrorMessage = "Полетата за новата парола и потвърждението й не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }
}
