namespace ElementsWeb.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ElementsWeb.Common;
    using ElementsWeb.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<ElementsWeb.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ElementsWeb.Data.ApplicationDbContext context)
        {
            const string Name = "admin";
            const string AdministratorUserName = "admin@admin.bg";
            const string AdministratorPassword = "1qaz@WSX";

            this.SeedAtributes(context);

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
                    UserName = Name,
                    Email = AdministratorUserName,
                };

                userManager.Create(user, AdministratorPassword);

                // user = userManager.FindByName(AdministratorUserName);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);

                this.SeedUsers(userManager, context);
            }

            if (!context.ServerSettings.Any())
            {
                ServerSettings firstServerSettings = new ServerSettings()
                {
                    Version = "0.0.1",
                    CreatedOn = DateTime.UtcNow,
                    URL = "noUrl"
                };

                context.ServerSettings.Add(firstServerSettings);
            }
        }

        private void SeedAtributes(ApplicationDbContext context)
        {
            if (!context.CharacterClasss.Any())
            {
                CharacterClass elemental = new CharacterClass()
                {
                    Name = "Elemental",
                    Description = "The force of the elements",
                    CreatedOn = DateTime.UtcNow
                };

                CharacterClass warrior = new CharacterClass()
                {
                    Name = "Warrior",
                    Description = "Mighty hero",
                    CreatedOn = DateTime.UtcNow
                };

                context.CharacterClasss.Add(elemental);
                context.CharacterClasss.Add(warrior);

                if (!context.BaseAtributes.Any())
                {
                    context.BaseAtributes.Add(new BaseAtributes()
                    {
                        MaxHealth = 100,
                        MaxResource = 50,
                        HealthRegen = 1.2f,
                        ResourceRegen = 0.8f,
                        Agility = 0,
                        Armor = 2,
                        AttackDamage = 10,
                        MagicDamage = 11,
                        Intellect = 3,
                        Stamina = 2,
                        Strength = 0,
                        CharacterClass = context.CharacterClasss.FirstOrDefault(ch => ch.Name.Equals("Elemental"))
                    });

                    context.BaseAtributes.Add(new BaseAtributes()
                    {
                        MaxHealth = 110,
                        MaxResource = 100,
                        HealthRegen = 1.2f,
                        ResourceRegen = 0.0f,
                        Agility = 0,
                        Armor = 4,
                        AttackDamage = 20,
                        MagicDamage = 0,
                        Intellect = 0,
                        Stamina = 2,
                        Strength = 3,
                        CharacterClass = context.CharacterClasss.FirstOrDefault(ch => ch.Name.Equals("Warrior"))
                    });
                }
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

            var character = new Character
            {
                Name = "Peshkircho",
                User = user,
                Class = context.CharacterClasss.FirstOrDefault(ch => ch.Name.Equals("Elemental"))
            };

            user.Characters = new List<Character>();
            user.Characters.Add(character);
        }
    }
}
