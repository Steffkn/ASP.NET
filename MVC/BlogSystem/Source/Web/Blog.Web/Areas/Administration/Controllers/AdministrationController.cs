namespace Blog.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Blog.Common;
    using Blog.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
