namespace DDS.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;

    public class ErrorController : BaseController
    {
        public ActionResult Error()
        {
            //this.Response.TrySkipIisCustomErrors = true;
            this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return this.View("Error");
        }

        public ActionResult PageNotFound()
        {
            //this.Response.TrySkipIisCustomErrors = true;
            this.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return this.View("PageNotFound");
        }
    }
}
