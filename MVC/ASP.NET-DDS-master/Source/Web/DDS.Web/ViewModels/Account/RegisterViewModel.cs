namespace DDS.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Data.Models;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Полето за име е задължително!")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Полето за презиме е задължително!")]
        [Display(Name = "Презиме")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Полето за фамилия е задължително!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето за е-майл е задължително!")]
        [EmailAddress]
        [Display(Name = "Е-майл")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Полето за потребителско име е задължително!")]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не е въведена парола!")]
        [StringLength(100, ErrorMessage = "{0} трябва да бъде поне {2} символа!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Не е въведено потвърждение на паролата!")]
        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = "Полетата за новата парола и потвърждението й не съвпадат.")]
        public string ConfirmPassword { get; set; }

        public Student Student { get; set; }
    }
}
