namespace DDS.Web.ViewModels.Manage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Microsoft.AspNet.Identity;

    public class IndexViewModel
    {
        public string UserId { get; set; }

        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public Student Student { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Фак. Номер")]
        public int FNumber { get; set; }

        [Display(Name = "Научни степени")]
        public string ScienceDegree { get; set; }
    }
}
