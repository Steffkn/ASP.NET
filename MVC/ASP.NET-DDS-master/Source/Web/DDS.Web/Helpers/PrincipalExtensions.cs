namespace DDS.Web.Helpers
{
    using System.Security.Claims;
    using DDS.Data.Models;
    using Microsoft.AspNet.Identity;

    public static class PrincipalExtensions
    {
        public static string PhoneNumber(this ClaimsPrincipal user, UserManager<ApplicationUser> userManager)
        {
            if (user.Identity.IsAuthenticated)
            {
                var appUser = userManager.FindByIdAsync(user.Identity.GetUserId()).Result;

                return appUser.PhoneNumber;
            }

            return "";
        }

        public static string Email(this ClaimsPrincipal user, UserManager<ApplicationUser> userManager)
        {
            if (user.Identity.IsAuthenticated)
            {
                var appUser = userManager.FindByIdAsync(user.Identity.GetUserId()).Result;

                return appUser.Email;
            }

            return string.Empty;
        }
    }
}
