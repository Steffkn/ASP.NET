namespace ElementsWeb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ElementsWeb.Common;
    using ElementsWeb.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<ElementsWeb.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ElementsWeb.Data.ApplicationDbContext context)
        {
            const string Name = "admin";
            const string AdministratorUserName = Name;
            const string AdministratorPassword = "1qaz@WSX";

            if (!context.Roles.Any())
            {
                // Create user, moderator, admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(role);
                role = new IdentityRole { Name = GlobalConstants.ModeratorRoleName };
                roleManager.Create(role);
                role = new IdentityRole { Name = GlobalConstants.UserRoleName };
                roleManager.Create(role);
            }

            if (!context.Users.Any())
            {
                // Create admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                };

                userManager.Create(user, AdministratorPassword);

                //user = userManager.FindByName(AdministratorUserName);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);

                this.SeedUsers(userManager, context);
            }

            if (!context.ServerSettings.Any())
            {
                ServerSettings firstServerSettings = new ServerSettings()
                {
                    Version = "1.0.0",
                    CreatedOn = DateTime.UtcNow,
                    URL = "noUrl"
                };
            }
        }

        private void SeedUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            var user = new ApplicationUser
            {
                UserName = "steff",
                Email = "m.hunter@abv.bg",
            };
            userManager.Create(user, "1qaz@WSX");

            // Assign user to role
            userManager.AddToRole(user.Id, GlobalConstants.UserRoleName);

            user = new ApplicationUser
            {
                UserName = "test",
                Email = "test@test.bg"
            };

            userManager.Create(user, "1qaz@WSX");

            // Assign user to role
            userManager.AddToRole(user.Id, GlobalConstants.UserRoleName);

            Random rand = new Random();
            var character = new Character
            {
                Name = "Peshkircho",
                RandomNumber = rand.Next(1000),
                User = user,
                MaxHealth = 100,
                MaxResource = 80,
            };

            user.Characters = new List<Character>();
            user.Characters.Add(character);
        }
    }
}
