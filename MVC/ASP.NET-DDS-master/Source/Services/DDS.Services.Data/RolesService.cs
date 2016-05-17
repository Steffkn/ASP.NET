namespace DDS.Services.Data
{
    using System.Linq;
    using DDS.Data;
    using Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RolesService : IRolesService
    {
        private ApplicationDbContext db;
        private RoleStore<IdentityRole> roleStore;
        private RoleManager<IdentityRole> roleManager;

        public RolesService()
        {
            this.db = new ApplicationDbContext();
            this.roleStore = new RoleStore<IdentityRole>(this.db);
            this.roleManager = new RoleManager<IdentityRole>(this.roleStore);
        }

        public IQueryable<IdentityRole> GetAll()
        {
            return this.roleManager.Roles;
        }

        public string GetById(string roleId)
        {
            return this.roleManager.FindById(roleId).Name;
        }
    }
}
