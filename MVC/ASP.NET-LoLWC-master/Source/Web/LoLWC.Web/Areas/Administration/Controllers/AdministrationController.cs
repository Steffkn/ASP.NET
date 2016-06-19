namespace LoLWC.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using LoLWC.Common;
    using LoLWC.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
