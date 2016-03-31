namespace DDS.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using DDS.Common;
    using DDS.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
