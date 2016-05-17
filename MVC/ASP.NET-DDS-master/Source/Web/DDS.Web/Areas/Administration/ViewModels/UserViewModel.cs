namespace DDS.Web.Areas.Administration.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [DisplayName("Име")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Емейл")]
        public string Email { get; set; }

        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

        [DisplayName("Псевдоним")]
        public string UserName { get; set; }

        [DisplayName("Права")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
