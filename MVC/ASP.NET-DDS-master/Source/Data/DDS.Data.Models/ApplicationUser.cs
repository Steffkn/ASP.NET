namespace DDS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Моля въведете име.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Моля въведете презиме.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Моля въведете фамилия.")]
        public string MiddleName { get; set; }

        public string ScienceDegree { get; set; }

        // what will happen with Ph.D. (maybe adding in future) this will be better solution
        public Student Student { get; set; }

        public Teacher Teacher { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
