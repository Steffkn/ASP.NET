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
                FirstName = "���������",
                MiddleName = "����������",
                LastName = "����-��������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "niko",
                FirstName = "������",
                MiddleName = "������",
                LastName = "��������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "cveti",
                FirstName = "���������",
                MiddleName = "���������",
                LastName = "������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "rali",
                FirstName = "������",
                MiddleName = "�������",
                LastName = "��������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "steff",
                FirstName = "������",
                MiddleName = "�������",
                LastName = "�������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "krisi",
                FirstName = "��������",
                MiddleName = "��������",
                LastName = "��������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "stamat",
                FirstName = "������",
                MiddleName = "����������",
                LastName = "����������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "drago",
                FirstName = "��������",
                MiddleName = "������",
                LastName = "�������",
            });

            users.Add(new SimpleUser()
            {
                UserName = "boji",
                FirstName = "��������",
                MiddleName = "��������",
                LastName = "�������",
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
                "���._�-� ���� ������������ ����������� anni",
                "���._�-� ���� ������������ ������ boqn",
                "���._�-� ��������� ����������� ��������� valentina",
                "���._�-� ������ ������� ������� plamen",
                "��._��._�-� �������� �������� ��������� viki",
                "��._��._�-� ������� �������� ����������� dani",
                "��._��._�-� ����� �������� �������� biser",
                "��._��. ��� ��������� ����������-������ zoq",
                "��._��. ����� ������ ������� boris",
                "��. ������ �������� �������� zahari",
                "��. ������� ������� �������� adi",
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
            //    FirstName = "������",
            //    MiddleName = "���������",
            //    LastName = "�������",
            //    ScienceDegree = ""
            //});
            //teachers.Add(new SimpleUser()
            //{
            //    UserName = "kosi",
            //    FirstName = "�����������",
            //    MiddleName = "��������",
            //    LastName = "��������",
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
                Title = "���������� �� ��������� ����������� �� ���������� �������� ���� ������������ �� �������� ���������� ��� ������� ������ (������ � �������� 2009-10-02)",
                Description = "���������� �� ��������� ����������� �� ���������� ��������",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "����������;��������� �����������;O�������",
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
                Title = "���������� �� �������� ���������� ��� ������� ������ ",
                Description = "���������� �� �������� ���������� ��� ������� ������ ���������� �� �������� ���������� ��� ������� ������ ",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������;����������;��������",
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
                Title = "���������� �� ��������� ���������� (�������, ������) � ������������ ��������",
                Description = "���������� �� ��������� ���������� (�������, ������) � ������������ �������� (���� ���� � ���-���� � �������� �� ����������)",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "��������� ����������;������������ ��������;����������;��������",
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
                Title = "���������� �� ��������� ���������� (������) � �������� �� ����������� ����������� ",
                Description = "������/������������� ����� ����� � ������������ �� �����������, ���������� � ����������� ����������� (����� �� ������������, ����������� �� ������� �������, ��������� �� �����������, �� ���������� �� ����, � �����)",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "��������� ����������;������������ ��������;����������;O�������",
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
                Title = "����� �� ����� � ���������� ������������ ����� ������� �� ���������� ��������",
                Description = "������/������������� ������� ����������� � ����������/��������� �� ����� �� ����������� ������������ � ��������� �� ���������� ��������.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "��������� ����������;������������ ��������;����������;��������",
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
                Title = "���������� �� RCP ������� (plugin-s ��� NetBeans ��� Eclipse), ���� �����, ������, java �������� ����������",
                Description = "����������� � ���������� �� ����������, �������� �� ��������� ���������",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "RCP �������;NetBeans � Eclipse;����������;��������",
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
                Title = "����������� �� ������ � ������� ��������� ",
                Description = "����������� � ���������� �� ����������, �� ������ � ������� ���������.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������� ���������;�����������;����������;����������;��������",
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
                Title = "������������ �� �������� Web 2.0 ����������� ",
                Description = "������/������������� ������� �������������� �� ��������� ����������� � ���������� �� java �������� ����������.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "�����������;Web 2.0;����������;��������",
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
                Title = "���������� �� ��������� ����������� �� ���������� (monitoring) �������� �� ��� ������� - � �������� �������",
                Description = "���������� �� ��������� ����������� �� ���������� (monitoring) �������� �� ��� ������� - � �������� �������: �������� ����������������, ����������, ����������, ���������(2015)",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "�����������;��������� �����������;����������",
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
                Title = "���������� �� ��������� ����������� �� ������ �� ��������� �������",
                Description = "���������� �� ��������� ����������� �� ������ �� ��������� ������� - ��� �������� ��� �������� (2015)",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "�����������;������ �� ��������� �������;����������",
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
                Title = "���������� �� ����� �� ������ �� ������������ � ������������ �������� �����",
                Description = "���������� �� ���������� ����� �� ��������� � �������� ��� ������ �� ��������� �� ��������� �� ����������� � �������. ",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������ �� ����� �� ������;�����������;����������",
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
                Title = "���������� �� ����� �� ������ �� ���������� � ������� �����",
                Description = "���������� �� ���������� ����� �� ��������� � �������� ��� ������ �� ���������.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������ �� ����� �� ������;�����������;����������",
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
                Title = "��������� �������� � ���� �� ����������� �������� �� �������������� �������",
                Description = "������ � ���� �� ������������ �� ���-������ ����, � ���� ��������������, ������ ��������� ����.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������ �� ����;�����������;����������",
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
                Title = "���������� �� ��������� ����������� �� ������������ � ������������ ����������",
                Description = "����� � �� �� ��������� ������� ���� �� ��������� �������������� �������� � ������� ��������� �������������� �������� �� ����������� ����������� �� ������������ � ������������ ����������.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������ �� ������� �������;�����������;����������",
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
                Title = "��������� ������� �� ������������ � ��������� �� �������� �������",
                Description = "���������� �� ��������� ������� �� ������������ � ��������� �� �������� �������",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������ �� ������� �������. ���� � ������.;����� �� ������������ ����������.;����������� �� ���������.;��������� ����������.;����������",
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
                Title = "��������� ������� �� ����������� � ��������� �� ���� �����",
                Description = "���������� �� ��������� ������� �� ����������� � ��������� �� ���� �����",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������ �� ������� �������. ���� � ������.;����� �� ������������ ����������.;����������� �� ���������.;��������� ����������.;����������",
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
                Title = "��������� ������� �� ������������ �� �������� �������",
                Description = "���������� �� ��������� ������� �� ������������ �� �������� �������",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������ �� ������� �������. ���� � ������.;����� �� ������������ ����������.;����������� �� ���������.;��������� ����������.;����������",
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
