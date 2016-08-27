namespace DDS.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using App_Start;
    using Data.Models;
    using DDS.Common;
    using DDS.Web.Controllers;
    using DDS.Web.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data.Interfaces;
    using ViewModels;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
        private readonly IAdministrationService users;

        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;

        public AdministrationController(
            IAdministrationService users)
        {
            this.users = users;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this.roleManager ?? this.Request.GetOwinContext().Get<ApplicationRoleManager>();
            }

            private set
            {
                this.roleManager = value;
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public ActionResult Index()
        {
            var allusers = this.users.GetAll().ToList();
            var viewModel = allusers.Select(
                user => new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    RolesList = this.UserManager.GetRoles(user.Id)
                                                .Select(role => new SelectListItem { Text = role, Value = role, Selected = true })
                })
                    .ToList();

            return this.View(viewModel);
        }

        // GET: Administration/Administration/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = this.users.GetById(id);
            if (user == null)
            {
                return this.HttpNotFound();
            }

            var allRoles = this.RoleManager.Roles.Select(
               role => new SelectListItem
               {
                   Text = role.Name,
                   Value = role.Name,
                   Selected = false
               });

            UserViewModel userViewModel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RolesList = this.UserManager.GetRoles(user.Id)
                                            .Select(role => new SelectListItem { Text = role, Value = role, Selected = true })
                                            .Concat(allRoles)
                                            .GroupBy(r => r.Text)
                                            .Select(grp => grp.FirstOrDefault())
                                            .OrderBy(r => r.Text)
                                            .ToList()
            };

            return this.View(userViewModel);
        }

        // POST: Administration/UserViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,UserName,RolesValues")] UserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userRoles = await this.UserManager.GetRolesAsync(model.Id);
                var result = await this.UserManager.AddToRolesAsync(model.Id, model.RolesValues.Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    this.ModelState.AddModelError("", result.Errors.First());
                    return this.View();
                }

                result = await this.UserManager.RemoveFromRolesAsync(model.Id, userRoles.Except(model.RolesValues).ToArray());

                if (!result.Succeeded)
                {
                    this.ModelState.AddModelError("", result.Errors.First());
                    return this.View();
                }

                var user = this.UserManager.FindById(model.Id);
                var teacher = this.users.GetAllTeachers().FirstOrDefault(t => t.User.Id == model.Id);

                if (this.UserManager.IsInRole(model.Id, GlobalConstants.TeacherRoleName))
                {
                    if (teacher == null)
                    {
                        user.Teacher = new Teacher()
                        {
                            CreatedOn = DateTime.Now,
                        };

                        this.UserManager.Update(user);
                    }
                    else
                    {
                        teacher.IsDeleted = false;
                        teacher.ModifiedOn = DateTime.Now;
                        teacher.DeletedOn = null;
                        this.users.EditTeacher(teacher);
                    }
                }
                else
                {
                    if (teacher != null)
                    {
                        teacher.IsDeleted = true;
                        teacher.DeletedOn = DateTime.Now;
                        this.users.EditTeacher(teacher);
                    }
                }

                var student = this.users.GetAllStudents().FirstOrDefault(t => t.User.Id == model.Id);

                if (this.UserManager.IsInRole(model.Id, GlobalConstants.StudentRoleName))
                {
                    if (student == null)
                    {
                        user.Student = new Student()
                        {
                            CreatedOn = DateTime.Now,
                        };

                        this.UserManager.Update(user);
                    }
                    else
                    {
                        student.IsDeleted = false;
                        student.ModifiedOn = DateTime.Now;
                        student.DeletedOn = null;
                        this.users.EditStudent(student);
                    }
                }
                else
                {
                    if (student != null)
                    {
                        student.IsDeleted = true;
                        student.DeletedOn = DateTime.Now;
                        this.users.EditStudent(student);
                    }
                }

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}
