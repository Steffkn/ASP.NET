namespace DDS.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using DDS.Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(DDS.Data.ApplicationDbContext context)
        {
            const string Name = "Admin";
            const string AdministratorUserName = "admin@admin.com";
            const string AdministratorPassword = AdministratorUserName;

            if (!context.Roles.Any())
            {
                // Create student, teacher, admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var role = new IdentityRole { Name = GlobalConstants.StudentRoleName };
                roleManager.Create(role);
                role = new IdentityRole { Name = GlobalConstants.TeacherRoleName };
                roleManager.Create(role);
                role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
                roleManager.Create(role);

                // Create admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    FirstName = Name,
                    LastName = Name
                };
                userManager.Create(user, AdministratorPassword);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);

                this.SeedUsers(userManager, context);
                this.SeedDiplomas(userManager, context);
            }
        }

        private void SeedUsers(UserManager<ApplicationUser> userManager, DDS.Data.ApplicationDbContext context)
        {
            var hasher = new PasswordHasher();

            var userName = "misheto98";
            var userPassword = "1qaz@WSX";
            var firstName = "Маргарита";
            var middleName = "Стоименова";
            var lastName = "Кара-Качанова";

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
            };

            var student = new Student()
            {
                User = user
            };

            var teacher = new Teacher()
            {
                User = user
            };

            //user.Teacher = new Teacher()
            //{
            //    CreatedOn = DateTime.Now,
            //};

            context.Students.Add(student);
            context.Teachers.Add(teacher);
            context.SaveChanges();

            //userManager.Update(user);

            userManager.AddToRole(user.Id, GlobalConstants.StudentRoleName);
            userManager.AddToRole(user.Id, GlobalConstants.TeacherRoleName);

            userName = "niko";
            firstName = "Никола";
            middleName = "Иванов";
            lastName = "Захариев";

            user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
            };

            student = new Student()
            {
                User = user
            };

            context.Students.Add(student);
            context.SaveChanges();

            userManager.AddToRole(user.Id, GlobalConstants.StudentRoleName);

            userName = "cveti";
            firstName = "Цветелина";
            middleName = "Георгиева";
            lastName = "Попова";

            user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
                ScienceDegree = "гл.ас."
            };

            student = new Student()
            {
                User = user
            };

            context.Students.Add(student);
            context.SaveChanges();
            userManager.AddToRole(user.Id, GlobalConstants.StudentRoleName);

            userName = "rali";
            firstName = "Ралица";
            middleName = "Спасова";
            lastName = "Стоянова";

            user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
                ScienceDegree = "ас."
            };

            teacher = new Teacher()
            {
                User = user
            };

            context.Teachers.Add(teacher);
            context.SaveChanges();
            userManager.AddToRole(user.Id, GlobalConstants.TeacherRoleName);

            userName = "steff";
            firstName = "Стефан";
            middleName = "Тодоров";
            lastName = "Чуфидов";

            user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
                ScienceDegree = "инж"
            };

            student = new Student()
            {
                User = user
            };

            context.Students.Add(student);
            context.SaveChanges();

            userManager.AddToRole(user.Id, GlobalConstants.StudentRoleName);
        }

        private void SeedDiplomas(UserManager<ApplicationUser> userManager, DDS.Data.ApplicationDbContext context)
        {
            List<Tag> allTags = new List<Tag>();
            allTags.Add(context.Tags.Add(new Tag() { Name = "HTML" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "CSS" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "Java" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "JavaScript" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "CoffeeScript" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "SASS" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "LESS" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "C++" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "C" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "C#" }));

            context.SaveChanges();

            var userTeacher = userManager.FindByName("misheto98").Teacher;
            var teacher = context.Teachers.FirstOrDefault(t => t.Id == userTeacher.Id);
            var count = teacher.Tags.Count;

            var diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[4]);
            diplomaTags.Add(allTags[5]);

            var diploma = new Diploma()
            {
                Title = "Разработка на софтуерни инструменти за електронно обучение чрез използването на портални приложения или отделни модули (темата е добавена 2009-10-02)",
                Description = "Разработка на софтуерни инструменти за електронно обучение",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Разработка,Софтуерни инструменти,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[1]);
            diplomaTags.Add(allTags[3]);
            diplomaTags.Add(allTags[7]);

            diploma = new Diploma()
            {
                Title = "Разработка на портални приложения или отделни модули ",
                Description = "Разработка на портални приложения или отделни модули Разработка на портални приложения или отделни модули ",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "модули,приложения,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[0]);
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[8]);
            diplomaTags.Add(allTags[4]);

            diploma = new Diploma()
            {
                Title = "Разработка на софтуерни инстументи (системи, услуги) в електронното обучение",
                Description = "Разработка на софтуерни инстументи (системи, услуги) в електронното обучение (като цяло и най-вече в областта на тестването)",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "софтуерни инстументи,електронното обучение,приложения,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Разработка на софтуерни инстументи (услуги) в областта на софтуерното инженерство ",
                Description = "Темата/направлението вючва обзор и разработване на инструменти, използвани в софтуерното инженерство (среди за програмиране, инструменти за тестови единици, редактори за рефакторинг, за управление на кода, и други)",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "софтуерни инстументи,електронното обучение,приложения,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Обмен на данни и оперативна съвместимост между системи за електронно обучение",
                Description = "Темата/направлението включва запознаване и реализация/адаптация на някоя от популярните спецификации и стандарти за електронно обучение (като например портфолио, пакетиране на съдържанието, учебни обекти, стандарт за оценяване), голям набор от спецификации са налични на адрес: http://www.imsglobal.org/specificationdownload.cfm",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "софтуерни инстументи,електронното обучение,приложения,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Разработка на RCP клиенти (plugin-s към NetBeans или Eclipse), бази данни, услуги, java сървърни приложения",
                Description = "Проектиране и разработка на приложения, базирани на софтуерни приставки",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "RCP клиенти ,NetBeans,Eclipse,приложения,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Инструменти за работа с цифрови хранилища ",
                Description = "Проектиране и разработка на приложения, за работа с цифрови хранилища.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "цифрови хранилища ,Проектиране,разработка,приложения,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            userTeacher = userManager.FindByName("rali").Teacher;
            teacher = context.Teachers.FirstOrDefault(t => t.Id == userTeacher.Id);

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Разработване на социални Web 2.0 инструменти ",
                Description = "Темата/направлението включва разработването на софтуерни инструменти с използване на java сървърни технологии.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Проектиране,Web 2.0,приложения,обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Разработка на софтуерни инструменти за наблюдение (monitoring) работата на уеб системи - в различни аспекти",
                Description = "Разработка на софтуерни инструменти за наблюдение (monitoring) работата на уеб системи - в различни аспекти: например производителност, надеждност, стабилност, сигурност(2015)",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Проектиране,Софтуерни инструменти,Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Разработка на софтуерни инструменти за анализ на софтуерни системи",
                Description = "Разработка на софтуерни инструменти за анализ на софтуерни системи - уеб базирани или настолни (2015)",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Проектиране,анализ на софтуерни системи,Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }
        }
    }
}
