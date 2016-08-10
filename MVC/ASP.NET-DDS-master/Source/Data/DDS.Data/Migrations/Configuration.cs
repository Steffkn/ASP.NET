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
            var firstName = "���������";
            var middleName = "����������";
            var lastName = "����-��������";

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
            firstName = "������";
            middleName = "������";
            lastName = "��������";

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
            firstName = "���������";
            middleName = "���������";
            lastName = "������";

            user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
                ScienceDegree = "��.��."
            };

            student = new Student()
            {
                User = user
            };

            context.Students.Add(student);
            context.SaveChanges();
            userManager.AddToRole(user.Id, GlobalConstants.StudentRoleName);

            userName = "rali";
            firstName = "������";
            middleName = "�������";
            lastName = "��������";

            user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
                ScienceDegree = "��."
            };

            teacher = new Teacher()
            {
                User = user
            };

            context.Teachers.Add(teacher);
            context.SaveChanges();
            userManager.AddToRole(user.Id, GlobalConstants.TeacherRoleName);

            userName = "steff";
            firstName = "������";
            middleName = "�������";
            lastName = "�������";

            user = new ApplicationUser
            {
                UserName = userName,
                Email = userName + "@abv.bg",
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(userPassword),
                ScienceDegree = "���"
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
                Title = "���������� �� ��������� ����������� �� ���������� �������� ���� ������������ �� �������� ���������� ��� ������� ������ (������ � �������� 2009-10-02)",
                Description = "���������� �� ��������� ����������� �� ���������� ��������",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "����������,��������� �����������,��������",
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
                ContentCSV = "������,����������,��������",
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
                ContentCSV = "��������� ����������,������������ ��������,����������,��������",
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
                Title = "���������� �� ��������� ���������� (������) � �������� �� ����������� ����������� ",
                Description = "������/������������� ����� ����� � ������������ �� �����������, ���������� � ����������� ����������� (����� �� ������������, ����������� �� ������� �������, ��������� �� �����������, �� ���������� �� ����, � �����)",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "��������� ����������,������������ ��������,����������,��������",
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
                Title = "����� �� ����� � ���������� ������������ ����� ������� �� ���������� ��������",
                Description = "������/������������� ������� ����������� � ����������/��������� �� ����� �� ����������� ������������ � ��������� �� ���������� �������� (���� �������� ���������, ���������� �� ������������, ������ ������, �������� �� ���������), ����� ����� �� ������������ �� ������� �� �����: http://www.imsglobal.org/specificationdownload.cfm",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "��������� ����������,������������ ��������,����������,��������",
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
                Title = "���������� �� RCP ������� (plugin-s ��� NetBeans ��� Eclipse), ���� �����, ������, java �������� ����������",
                Description = "����������� � ���������� �� ����������, �������� �� ��������� ���������",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "RCP ������� ,NetBeans,Eclipse,����������,��������",
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
                Title = "����������� �� ������ � ������� ��������� ",
                Description = "����������� � ���������� �� ����������, �� ������ � ������� ���������.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "������� ��������� ,�����������,����������,����������,��������",
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
                Title = "������������ �� �������� Web 2.0 ����������� ",
                Description = "������/������������� ������� �������������� �� ��������� ����������� � ���������� �� java �������� ����������.",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "�����������,Web 2.0,����������,��������",
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
                Title = "���������� �� ��������� ����������� �� ���������� (monitoring) �������� �� ��� ������� - � �������� �������",
                Description = "���������� �� ��������� ����������� �� ���������� (monitoring) �������� �� ��� ������� - � �������� �������: �������� ����������������, ����������, ����������, ���������(2015)",
                ExperimentalPart = "��������� ���, CD",
                ContentCSV = "�����������,��������� �����������,����������",
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
                ContentCSV = "�����������,������ �� ��������� �������,����������",
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
