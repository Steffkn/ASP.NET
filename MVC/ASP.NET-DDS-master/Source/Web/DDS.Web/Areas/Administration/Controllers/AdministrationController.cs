namespace DDS.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using App_Start;
    using Data;
    using DDS.Common;
    using DDS.Web.Controllers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Services.Data;
    using Services.Data.Interfaces;
    using ViewModels;
    using DDS.Web.Infrastructure;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
        private readonly IAdministrationService users;

        private ApplicationDbContext context = new ApplicationDbContext();
        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;

        public AdministrationController(
            IAdministrationService users,
            IRolesService roles)
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
                Email = user.Email,
                LastName = user.LastName,
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,PhoneNumber,UserName,RolesList")] UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userRoles = await UserManager.GetRolesAsync(model.Id);

                IList<string> roles = new List<string>();

                foreach (var role in model.RolesList)
                {
                    if (role.Selected)
                    {
                        roles.Add(role.Text);
                    }
                }

                //selectedRole = selectedRole ?? new string[] { };

                var result = await this.UserManager.AddToRolesAsync(model.Id,
                    roles.ToArray().Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                result = await this.UserManager.RemoveFromRolesAsync(model.Id,
                    userRoles.Except(roles).ToArray<string>());
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                //this.userManager.AddToRole(userModel.Id);
                //db.Entry(userModel).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
