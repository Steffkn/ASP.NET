namespace DDS.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Е-майл")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде поне {2} символа!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "Полетата за новата парола и потвърждението й не съвпадат.")]
        public string ConfirmPassword { get; set; }

        public Student Student { get; set; }
    }
}
