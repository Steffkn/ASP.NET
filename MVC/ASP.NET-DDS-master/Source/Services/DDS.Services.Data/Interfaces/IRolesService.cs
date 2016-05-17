namespace DDS.Services.Data.Interfaces
{
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IRolesService
    {
        IQueryable<IdentityRole> GetAll();

        string GetById(string roleId);
    }
}
