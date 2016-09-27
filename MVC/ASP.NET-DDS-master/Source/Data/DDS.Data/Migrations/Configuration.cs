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
                role = new IdentityRole { Name = GlobalConstants.ManagementRoleName };
                roleManager.Create(role);

                // Create admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    FirstName = Name,
                    MiddleName = Name,
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
            var userPassword = "1qaz@WSX";

            List<SimpleUser> users = new List<SimpleUser>();

            users.Add(new SimpleUser()
            {
                UserName = "misheto98",
                FirstName = "Маргарита",
                MiddleName = "Стоименова",
                LastName = "Кара-Качанова",
            });

            users.Add(new SimpleUser()
            {
                UserName = "niko",
                FirstName = "Никола",
                MiddleName = "Иванов",
                LastName = "Захариев",
            });

            users.Add(new SimpleUser()
            {
                UserName = "cveti",
                FirstName = "Цветелина",
                MiddleName = "Георгиева",
                LastName = "Попова",
            });

            users.Add(new SimpleUser()
            {
                UserName = "rali",
                FirstName = "Ралица",
                MiddleName = "Спасова",
                LastName = "Стоянова",
            });

            users.Add(new SimpleUser()
            {
                UserName = "steff",
                FirstName = "Стефан",
                MiddleName = "Тодоров",
                LastName = "Чуфидов",
            });

            users.Add(new SimpleUser()
            {
                UserName = "krisi",
                FirstName = "Кристина",
                MiddleName = "Малинова",
                LastName = "Капинова",
            });

            users.Add(new SimpleUser()
            {
                UserName = "stamat",
                FirstName = "Стамат",
                MiddleName = "Валентинов",
                LastName = "Костадинов",
            });

            users.Add(new SimpleUser()
            {
                UserName = "drago",
                FirstName = "Драгомир",
                MiddleName = "Иванов",
                LastName = "Тодоров",
            });

            users.Add(new SimpleUser()
            {
                UserName = "boji",
                FirstName = "Божидаря",
                MiddleName = "Маринова",
                LastName = "Таскова",
            });

            foreach (var user in users)
            {
                var appUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.UserName + "@abv.bg",
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(userPassword),
                    ScienceDegree = user.ScienceDegree,
                };

                context.Students.Add(new Student() { User = appUser });
                context.SaveChanges();

                userManager.AddToRole(appUser.Id, GlobalConstants.StudentRoleName);
            }

            List<SimpleUser> teachers = new List<SimpleUser>();
            string[] teachersDB =
            {
                "Доц._д-р Анна Светославова Добромирова anni",
                "Доц._д-р Боян Константинов Крумов boqn",
                "Доц._д-р Валентина Цветомирова Симеонова valentina",
                "Доц._д-р Пламен Руменов Стоянов plamen",
                "Гл._ас._д-р Виктория Христова Георгиева viki",
                "Гл._ас._д-р Даниела Сергеева Костадинова dani",
                "Гл._ас._д-р Бисер Стаменов Цветанов biser",
                "Гл._ас. Зоя Чавдарова Аспарухова-Колева zoq",
                "Гл._ас. Борис Райков Маринов boris",
                "Ас. Захари Григоров Пламенов zahari",
                "Ас. Адрияна Иванова Христова adi",
            };

            foreach (var person in teachersDB)
            {
                var teacher = person.Split(' ');
                teachers.Add(new SimpleUser()
                {
                    ScienceDegree = teacher[0].Replace('_', ' '),
                    FirstName = teacher[1],
                    MiddleName = teacher[2],
                    LastName = teacher[3],
                    UserName = teacher[4],
                });
            }

            //teachers.Add(new SimpleUser()
            //{
            //    UserName = "georgi",
            //    FirstName = "Георги",
            //    MiddleName = "Божидаров",
            //    LastName = "Стоянов",
            //    ScienceDegree = ""
            //});
            //teachers.Add(new SimpleUser()
            //{
            //    UserName = "kosi",
            //    FirstName = "Константина",
            //    MiddleName = "Кирилова",
            //    LastName = "Соколова",
            //});

            foreach (var user in teachers)
            {
                var appUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.UserName + "@abv.bg",
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = hasher.HashPassword(userPassword),
                    ScienceDegree = user.ScienceDegree,
                };

                context.Teachers.Add(new Teacher() { User = appUser });
                context.SaveChanges();

                userManager.AddToRole(appUser.Id, GlobalConstants.TeacherRoleName);
            }
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
            allTags.Add(context.Tags.Add(new Tag() { Name = "MVC" }));
            allTags.Add(context.Tags.Add(new Tag() { Name = "SQL" }));

            context.SaveChanges();

            var userTeacher = userManager.FindByName("boqn").Teacher;
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
                ContentCSV = "Разработка;Софтуерни инструменти;Oбучение",
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
                ContentCSV = "Модули;Приложения;Обучение",
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
                ContentCSV = "Софтуерни инстументи;Електронното обучение;Приложения;Обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[3]);
            diplomaTags.Add(allTags[4]);

            diploma = new Diploma()
            {
                Title = "Разработка на софтуерни инстументи (услуги) в областта на софтуерното инженерство ",
                Description = "Темата/направлението вючва обзор и разработване на инструменти, използвани в софтуерното инженерство (среди за програмиране, инструменти за тестови единици, редактори за рефакторинг, за управление на кода, и други)",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Софтуерни инстументи;Електронното обучение;Приложения;Oбучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            userTeacher = userManager.FindByName("dani").Teacher;
            teacher = context.Teachers.FirstOrDefault(t => t.Id == userTeacher.Id);

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[6]);
            diplomaTags.Add(allTags[7]);
            diplomaTags.Add(allTags[4]);

            diploma = new Diploma()
            {
                Title = "Обмен на данни и оперативна съвместимост между системи за електронно обучение",
                Description = "Темата/направлението включва запознаване и реализация/адаптация на някоя от популярните спецификации и стандарти за електронно обучение.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Софтуерни инстументи;Електронното обучение;Приложения;Обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[4]);
            diplomaTags.Add(allTags[5]);

            diploma = new Diploma()
            {
                Title = "Разработка на RCP клиенти (plugin-s към NetBeans или Eclipse), бази данни, услуги, java сървърни приложения",
                Description = "Проектиране и разработка на приложения, базирани на софтуерни приставки",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "RCP клиенти;NetBeans и Eclipse;Приложения;Обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[7]);
            diplomaTags.Add(allTags[5]);
            diplomaTags.Add(allTags[6]);

            diploma = new Diploma()
            {
                Title = "Инструменти за работа с цифрови хранилища ",
                Description = "Проектиране и разработка на приложения, за работа с цифрови хранилища.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Цифрови хранилища;Проектиране;Разработка;Приложения;Обучение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            userTeacher = userManager.FindByName("viki").Teacher;
            teacher = context.Teachers.FirstOrDefault(t => t.Id == userTeacher.Id);

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[2]);
            diplomaTags.Add(allTags[4]);
            diplomaTags.Add(allTags[8]);

            diploma = new Diploma()
            {
                Title = "Разработване на социални Web 2.0 инструменти ",
                Description = "Темата/направлението включва разработването на софтуерни инструменти с използване на java сървърни технологии.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Проектиране;Web 2.0;Приложения;Обучение",
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
            diplomaTags.Add(allTags[4]);

            diploma = new Diploma()
            {
                Title = "Разработка на софтуерни инструменти за наблюдение (monitoring) работата на уеб системи - в различни аспекти",
                Description = "Разработка на софтуерни инструменти за наблюдение (monitoring) работата на уеб системи - в различни аспекти: например производителност, надеждност, стабилност, сигурност(2015)",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Проектиране;Софтуерни инструменти;Приложение",
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
                ContentCSV = "Проектиране;Анализ на софтуерни системи;Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            userTeacher = userManager.FindByName("plamen").Teacher;
            teacher = context.Teachers.FirstOrDefault(t => t.Id == userTeacher.Id);

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[7]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Разработка на метод за защита на информацията в корпоративна интернет среда",
                Description = "Анализират се вероятните атаки на системата и загубите при пробив на системата за сигурност на ведомството и фирмата. ",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Анализ на метод за защита;Проектиране;Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[7]);
            diplomaTags.Add(allTags[8]);

            diploma = new Diploma()
            {
                Title = "Разработка на метод за защита на информация в мрежова среда",
                Description = "Анализират се вероятните атаки на системата и загубите при пробив на системата.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Анализ на метод за защита;Проектиране;Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            userTeacher = userManager.FindByName("zahari").Teacher;
            teacher = context.Teachers.FirstOrDefault(t => t.Id == userTeacher.Id);

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[7]);
            diplomaTags.Add(allTags[9]);

            diploma = new Diploma()
            {
                Title = "Програмно средство и език за реализиране методите на интелигентните системи",
                Description = "Пролог е език за програмиране от най-високо ниво, с общо предназначение, широко използван днес.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Анализ на език;Проектиране;Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[7]);
            diplomaTags.Add(allTags[8]);

            diploma = new Diploma()
            {
                Title = "Разработка на виртуална лаборатория по мултимедийни и хипермедийни технологии",
                Description = "Целта е да се разработи система като се обосноват функционалните елементи и изберат подходящи инструментални средства за виртуалната лаборатория по мултимедийни и хипермедийни технологии.",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Анализ на подобни системи;Проектиране;Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            userTeacher = userManager.FindByName("anni").Teacher;
            teacher = context.Teachers.FirstOrDefault(t => t.Id == userTeacher.Id);

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[0]);
            diplomaTags.Add(allTags[1]);
            diplomaTags.Add(allTags[9]);
            diplomaTags.Add(allTags[10]);
            diplomaTags.Add(allTags[11]);

            diploma = new Diploma()
            {
                Title = "Софтуерна система за разпределяне и съставяне на дипломни задания",
                Description = "Разработка на софтуерна система за разпределяне и съставяне на дипломни задания",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Анализ на подобни системи. Цели и задачи.;Обзор на използваните технологии.;Проектиране на системата.;Програмна реализация.;Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }

            diplomaTags = new List<Tag>();
            diplomaTags.Add(allTags[9]);
            diplomaTags.Add(allTags[10]);
            diplomaTags.Add(allTags[11]);

            diploma = new Diploma()
            {
                Title = "Софтуерна система за проектиране и съставяне на база данни",
                Description = "Разработка на софтуерна система за проектиране и съставяне на база данни",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Анализ на подобни системи. Цели и задачи.;Обзор на използваните технологии.;Проектиране на системата.;Програмна реализация.;Приложение",
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
            diplomaTags.Add(allTags[1]);
            diplomaTags.Add(allTags[9]);
            diplomaTags.Add(allTags[10]);
            diplomaTags.Add(allTags[11]);

            diploma = new Diploma()
            {
                Title = "Софтуерна система за констроиране на дипломни задания",
                Description = "Разработка на софтуерна система за констроиране на дипломни задания",
                ExperimentalPart = "Програмен код, CD",
                ContentCSV = "Анализ на подобни системи. Цели и задачи.;Обзор на използваните технологии.;Проектиране на системата.;Програмна реализация.;Приложение",
                Teacher = userTeacher,
                Tags = diplomaTags
            };

            teacher.Diplomas.Add(diploma);

            foreach (var tag in diplomaTags)
            {
                teacher.Tags.Add(tag);
            }
        }

        private struct SimpleUser
        {
            public string UserName { get; set; }

            public string FirstName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }

            public string ScienceDegree { get; set; }
        }
    }
}
